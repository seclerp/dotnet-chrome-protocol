using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Nodes;
using ChromeProtocol.Core;
using ChromeProtocol.Runtime.Messaging.Json;

namespace ChromeProtocol.Runtime.Messaging.Logging;

public abstract class ProtocolClientLogger : IDisposable
{
  private readonly IProtocolClient _client;

  private readonly ProtocolClientLoggerOptions _options;

  // Contains methodName for particular request id.
  private readonly ConcurrentDictionary<int, string> _methodNamesMapping = new();

  protected ProtocolClientLogger(IProtocolClient client, ProtocolClientLoggerOptions options)
  {
    _client = client;
    _options = options;
  }

  public void StartLogging()
  {
    _client.OnConnected += ProcessConnected;
    _client.OnDisconnected += ProcessDisconnected;
    _client.OnRequestSent += ProcessOutgoingRequest;
    _client.OnResponseReceived += ProcessIncomingResponse;
    _client.OnEventReceived += ProcessIncomingEvent;
  }

  private void ProcessConnected(object sender, EventArgs args)
  {
    LogConnected("Protocol client connection estabilished");
  }

  private void ProcessDisconnected(object sender, EventArgs args)
  {
    LogDisconnected("Protocol client connection was closed");
  }

  private void ProcessOutgoingRequest(object sender, ProtocolRequest<ICommand> request)
  {
    var sessionId = GetPresentableSessionId(request.SessionId);
    var serializedMessage =
      TruncateIfNeeded(JsonSerializer.Serialize(request.Params, JsonProtocolSerialization.Settings));
    LogOutgoingRequest($"-> ({request.Id}) [{request.Method}] {{{sessionId}}}: {serializedMessage}");
    _methodNamesMapping.AddOrUpdate(request.Id, request.Method, (_, _) => request.Method);
  }

  private void ProcessIncomingResponse(object sender, ProtocolResponse<JsonObject> response)
  {
    var sessionId = GetPresentableSessionId(response.SessionId);
    if (!_methodNamesMapping.TryGetValue(response.Id, out var methodName))
      LogIncomingUnknownResponse($"Received an error of unknown request ID ({response.Id})");
    else if (response.Result is { } result)
    {
      var serializedMessage =
        TruncateIfNeeded(JsonSerializer.Serialize(result, JsonProtocolSerialization.Settings));
      LogIncomingResponse($"<- ({response.Id}) [{methodName}] {{{sessionId}}}: {serializedMessage}");
    }
    else if (response.Error is { } error)
    {
      var serializedMessage =
        TruncateIfNeeded(JsonSerializer.Serialize(error, JsonProtocolSerialization.Settings));
      LogIncomingError($"<! ({response.Id}) [{methodName}] {{{sessionId}}}: {serializedMessage}");
    }
  }

  private void ProcessIncomingEvent(object sender, ProtocolEvent<JsonObject> @event)
  {
    LogIncomingEvent($"<~ [{@event.Method}] {{{GetPresentableSessionId(@event.SessionId)}}}: {JsonSerializer.Serialize(@event.Params, JsonProtocolSerialization.Settings)}");
  }

  public void Dispose()
  {
    _client.OnConnected -= ProcessConnected;
    _client.OnDisconnected -= ProcessDisconnected;
    _client.OnRequestSent -= ProcessOutgoingRequest;
    _client.OnResponseReceived -= ProcessIncomingResponse;
    _client.OnEventReceived -= ProcessIncomingEvent;
  }

  public abstract void LogConnected(string message);

  public abstract void LogDisconnected(string message);

  public abstract void LogOutgoingRequest(string message);

  public abstract void LogIncomingResponse(string message);

  public abstract void LogIncomingUnknownResponse(string message);

  public abstract void LogIncomingError(string message);

  public abstract void LogIncomingEvent(string message);

  private string TruncateIfNeeded(string message) =>
    message.Length > _options.MaxMessageLengthChars && _options.MaxMessageLengthChars > 0
      ? $"{message.Substring(0, _options.MaxMessageLengthChars)}"
      : message;

  private static string GetPresentableSessionId(string? sessionId) => sessionId?.ToLowerInvariant().Substring(0, 5) ?? "<main>";
}
