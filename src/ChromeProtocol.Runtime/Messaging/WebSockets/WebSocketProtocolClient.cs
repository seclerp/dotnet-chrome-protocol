using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using ChromeProtocol.Core;
using ChromeProtocol.Runtime.Messaging.Json;
using Microsoft.Extensions.Logging;

namespace ChromeProtocol.Runtime.Messaging.WebSockets;

public class WebSocketProtocolClient<TNativeClient> : IProtocolClient
  where TNativeClient : WebSocket
{
  private readonly Uri _wsUri;
  private readonly ILogger _logger;
  private readonly ConcurrentDictionary<(string? sessionId, string eventName), Func<ProtocolEvent<JsonObject>, Task>> _eventHandlers = new ();
  private readonly ConcurrentDictionary<int, TaskCompletionSource<JsonObject>> _responseResolvers = new ();
  private readonly TNativeClient _nativeClient;
  private CancellationTokenSource? _connectionCancellation;
  private readonly BlockingCollection<ProtocolRequest<ICommand>> _outgoingMessages = new(new ConcurrentQueue<ProtocolRequest<ICommand>>());
  private int _currentId = 0;
  private bool _isDisposed = false;

  public WebSocketProtocolClient(TNativeClient nativeClient, Uri wsUri, ILogger logger)
  {
    _nativeClient = nativeClient;
    _wsUri = wsUri;
    _logger = logger;
  }

  public event EventHandler? OnConnected;
  public event EventHandler? OnDisconnected;

  public event EventHandler<ProtocolRequest<ICommand>>? OnRequestSent;
  public event EventHandler<ProtocolResponse<JsonObject>>? OnResponseReceived;
  public event EventHandler<ProtocolEvent<JsonObject>>? OnEventReceived;

  protected virtual Task InitializeConnectionAsync(TNativeClient nativeClient, Uri wsUri, CancellationToken token) =>
    Task.CompletedTask;

  public async Task ConnectAsync(CancellationToken token = default)
  {
    _connectionCancellation = new CancellationTokenSource();
    await InitializeConnectionAsync(_nativeClient, _wsUri, token).ConfigureAwait(false);

#pragma warning disable CS4014
    StartIncomingWorker(_connectionCancellation.Token);
      // .ContinueWith(task => _logger.LogError(task.Exception, "Error occured in incoming worker pump"), TaskContinuationOptions.OnlyOnFaulted);
    StartOutgoingWorker(_connectionCancellation.Token);
      // .ContinueWith(task => _logger.LogError(task.Exception, "Error occured in outgoing worker pump"), TaskContinuationOptions.OnlyOnFaulted);
#pragma warning restore CS4014
    OnConnected?.Invoke(this, EventArgs.Empty);
  }

  public async Task DisconnectAsync(CancellationToken token = default)
  {
    try
    {
      if (_nativeClient.State
          is not WebSocketState.Open
          and not WebSocketState.CloseSent
          and not WebSocketState.CloseReceived)
      {
        _logger.LogWarning(
          $"A graceful socket disconnect has been requested, but socket state is not valid for disconnect: {_nativeClient.State}");
      }
      else
      {
        _logger.LogInformation("Graceful socket disconnect has been requested");
        await _nativeClient.CloseAsync(WebSocketCloseStatus.Empty, null, token).ConfigureAwait(false);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Failed to disconnect the socket");
    }
    finally
    {
      Dispose();

      if (!_connectionCancellation?.IsCancellationRequested ?? false)
        OnDisconnected?.Invoke(this, EventArgs.Empty);
    }
  }

  public void ListenEvent<TEvent>(AsyncDomainEventHandler<TEvent> handler) where TEvent : IEvent
  {
    SubscribeAsync(handler);
  }

  public IDisposable SubscribeAsync<TEvent>(AsyncDomainEventHandler<TEvent> handler, string? sessionId = default) where TEvent : IEvent
  {
    Func<ProtocolEvent<JsonObject>, Task> HandleProtocolEvent(AsyncDomainEventHandler<TEvent> eventHandler) =>
      async rawEvent =>
      {
        var eventItself = rawEvent.Params.Deserialize<TEvent>(JsonProtocolSerialization.Settings);
        await eventHandler(eventItself).ConfigureAwait(false);
      };

    return SubscribeInternal<TEvent>(HandleProtocolEvent(handler), sessionId);
  }

  public IDisposable SubscribeSync<TEvent>(SyncDomainEventHandler<TEvent> handler, string? sessionId = default) where TEvent : IEvent
  {
    Func<ProtocolEvent<JsonObject>, Task> HandleProtocolEvent(SyncDomainEventHandler<TEvent> eventHandler) =>
      rawEvent =>
      {
        var eventItself = rawEvent.Params.Deserialize<TEvent>(JsonProtocolSerialization.Settings);
        return Task.Run(() => eventHandler(eventItself));
      };

    return SubscribeInternal<TEvent>(HandleProtocolEvent(handler), sessionId);
  }

  public async Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command,
    string? sessionId = null,
    CancellationToken? token = default) where TResponse : IType
  {
    var id = Interlocked.Increment(ref _currentId);
    try
    {
      var resolver = new TaskCompletionSource<JsonObject>();
      if (_responseResolvers.TryAdd(id, resolver))
      {
        await FireInternalAsync(id, GetMethodName(command.GetType()), command, sessionId).ConfigureAwait(false);
        var responseRaw = await resolver.Task.ConfigureAwait(false);
        var response = responseRaw.Deserialize<TResponse>(JsonProtocolSerialization.Settings) ?? throw new ArgumentException(null, nameof(responseRaw));
        return response;
      }

      throw new Exception();
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error occured while sending the protocol message");
      throw;
    }
  }

  public async Task FireCommandAsync(ICommand command, string? sessionId = default,
    CancellationToken token = default)
  {
    var id = Interlocked.Increment(ref _currentId);
    await FireInternalAsync(id, GetMethodName(command.GetType()), command, sessionId).ConfigureAwait(false);
  }

  public IScopedProtocolClient CreateScoped(string sessionId) => new ScopedProtocolClient(this, sessionId);

  public void Dispose()
  {
    if (_isDisposed) return;
    if (_connectionCancellation?.IsCancellationRequested ?? false) return;

    _connectionCancellation?.Cancel();
    _isDisposed = true;
  }

  private async Task FireInternalAsync(int id, string methodName, ICommand command, string? sessionId)
  {
    var request = new ProtocolRequest<ICommand>(id, methodName, command, sessionId);
    if (!_outgoingMessages.TryAdd(request))
    {
      throw new Exception("Can't schedule outgoing event for sending.");
    }
  }

  private IDisposable SubscribeInternal<TEvent>(Func<ProtocolEvent<JsonObject>, Task> rawHandler, string? sessionId) where TEvent : IEvent
  {
    var eventName = GetMethodName(typeof(TEvent));
    var subscription = new ProtocolSubscription<TNativeClient>(sessionId, eventName, rawHandler, this);
    _eventHandlers.AddOrUpdate((sessionId, eventName), rawHandler, (_, existing) => existing + rawHandler);
    return subscription;
  }

  private void StartOutgoingWorker(CancellationToken token)
  {
    new Thread(() =>
    {
      _logger.LogInformation("Starting outgoing messages pump...");
      while (!token.IsCancellationRequested)
      {
        try
        {
          var message = _outgoingMessages.Take(token);
          ProcessOutgoingRequest(message).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch (OperationCanceledException)
        {
          _logger.LogInformation("Cancellation requested, stopping outgoing queue thread...");
          return;
        }
      }
    }).Start();
  }

  private void StartIncomingWorker(CancellationToken token)
  {
    new Thread(() =>
    {
      _logger.LogInformation("Starting incoming messages pump...");

      try
      {
        var buffer = new byte[40096];
        using var memoryStream = new MemoryStream();

        while (_nativeClient.State == WebSocketState.Open && !token.IsCancellationRequested)
        {
          WebSocketReceiveResult incoming;

          do
          {
            incoming = _nativeClient.ReceiveAsync(new ArraySegment<byte>(buffer), token).ConfigureAwait(false)
              .GetAwaiter().GetResult();

            memoryStream.Write(buffer, 0, incoming.Count);
          } while (!incoming.EndOfMessage);

          var message = Encoding.UTF8.GetString(memoryStream.ToArray());
          memoryStream.Position = 0;
          memoryStream.SetLength(0);
          Task.Run(() => ProcessIncoming(message), token);
        }
      }
      catch (OperationCanceledException)
      {
        _logger.LogInformation("Cancellation requested, stopping incoming queue thread...");
      }
      catch (WebSocketException ex) when (ex.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
      {
        _logger.LogWarning($"Chrome has aborted the WebSocket connection during receive operation. Socket state: {_nativeClient.State}");
        DisconnectAsync(CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
      }
      catch (Exception e)
      {
        _logger.LogError(e, "Error occured while receiving the protocol message");
        throw;
      }
    }).Start();
  }

  private IProtocolMessage DeserializeMessage(string message)
  {
    var node = JsonNode.Parse(message) ?? throw new ArgumentException(null, nameof(message));

    if (node["id"] != null)
      return node.Deserialize<ProtocolResponse<JsonObject>>(JsonProtocolSerialization.Settings) ?? throw new ArgumentException(null, nameof(node));

    return node.Deserialize<ProtocolEvent<JsonObject>>(JsonProtocolSerialization.Settings) ?? throw new ArgumentException(null, nameof(node));
  }

  private Task ProcessIncoming(string message) =>
    DeserializeMessage(message) switch
    {
      ProtocolResponse<JsonObject> response => ProcessIncomingResponse(response),
      ProtocolEvent<JsonObject> @event => ProcessIncomingEvent(@event),
      _ => Task.CompletedTask
    };

  private async Task ProcessIncomingEvent(ProtocolEvent<JsonObject> @event)
  {
    OnEventReceived?.Invoke(this, @event);
    if (_eventHandlers.TryGetValue((@event.SessionId, @event.Method), out var handler))
      await handler.Invoke(@event).ConfigureAwait(false);
  }

  private async Task ProcessIncomingResponse(ProtocolResponse<JsonObject> response)
  {
    OnResponseReceived?.Invoke(this, response);
    _responseResolvers.TryRemove(response.Id, out var resolver);

    if (response.Error is { } error)
      resolver?.SetException(new ProtocolErrorException(error));

    if (response.Result is { } result)
      resolver?.SetResult(result);
  }

  private async Task ProcessOutgoingRequest(ProtocolRequest<ICommand> request)
  {
    var serialized = JsonSerializer.Serialize(request, JsonProtocolSerialization.Settings);
    var bytes = Encoding.UTF8.GetBytes(serialized);
    try
    {
      await _nativeClient.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, _connectionCancellation?.Token ?? CancellationToken.None).ConfigureAwait(false);
    }
    catch (WebSocketException ex) when (ex.WebSocketErrorCode == WebSocketError.InvalidState)
    {
      _logger.LogError($"Unsupported state detected when trying to send message to Chrome. Socket state: {_nativeClient.State}");
      await DisconnectAsync(CancellationToken.None).ConfigureAwait(false);
    }
    OnRequestSent?.Invoke(this, request);
  }

  private static string GetMethodName(MemberInfo type) =>
    type.GetCustomAttribute<MethodNameAttribute>()?.MethodName
    ?? throw new Exception($"{nameof(MethodNameAttribute)} is required on type {type.Name} but it is not presented.");

  private class ProtocolSubscription<T> : IDisposable where T : WebSocket
  {
    private readonly string? _sessionId;
    private readonly string _eventName;
    private readonly Func<ProtocolEvent<JsonObject>, Task>? _wrappedHandler;
    private readonly WebSocketProtocolClient<T> _client;

    public ProtocolSubscription(string? sessionId, string eventName, Func<ProtocolEvent<JsonObject>,Task>? wrappedHandler, WebSocketProtocolClient<T> client)
    {
      _sessionId = sessionId;
      _eventName = eventName;
      _wrappedHandler = wrappedHandler;
      _client = client;
    }

    public void Dispose()
    {
      var eventKey = (_sessionId, _eventName);
      if (_client._eventHandlers.TryGetValue(eventKey, out var aggregatedHandlers))
      {
        var updatedHandlers = aggregatedHandlers - _wrappedHandler;
        if (updatedHandlers is null)
        {
          _client._eventHandlers.TryRemove(eventKey, out _);
        }
        else
        {
          _client._eventHandlers[eventKey] = updatedHandlers;
        }
      }
    }
  }
}

