using System.Collections.Concurrent;
using ChromeProtocol.Core;
using ChromeProtocol.Runtime.Messaging.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChromeProtocol.Runtime.Messaging.Logging;

public abstract class ProtocolClientLogger : IDisposable
{
  private readonly IProtocolClient _client;
  // Contains methodName for particular request id.
  private readonly ConcurrentDictionary<int, string> _methodNamesMapping = new();

  protected ProtocolClientLogger(IProtocolClient client)
  {
    _client = client;
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
    LogOutgoingRequest($"-> ({request.Id}) [{request.Method}] {{{GetPresentableSessionId(request.SessionId)}}}: {JsonConvert.SerializeObject(request.Params, JsonProtocolSerialization.Settings)}");
    _methodNamesMapping.AddOrUpdate(request.Id, request.Method, (_, _) => request.Method);
  }

  private void ProcessIncomingResponse(object sender, ProtocolResponse<JObject> response)
  {
    if (!_methodNamesMapping.TryGetValue(response.Id, out var methodName))
      LogIncomingUnknownResponse($"Received an error of unknown request ID ({response.Id})");
    else if (response.Result is { } result)
      LogIncomingResponse($"<- ({response.Id}) [{methodName}] {{{GetPresentableSessionId(response.SessionId)}}}: {JsonConvert.SerializeObject(result, JsonProtocolSerialization.Settings)}");
    else if (response.Error is { } error)
      LogIncomingError($"<! ({response.Id}) [{methodName}] {{{GetPresentableSessionId(response.SessionId)}}}: {JsonConvert.SerializeObject(error, JsonProtocolSerialization.Settings)}");
  }

  private void ProcessIncomingEvent(object sender, ProtocolEvent<JObject> @event)
  {
    LogIncomingEvent($"<~ [{@event.Method}] {{{GetPresentableSessionId(@event.SessionId)}}}: {JsonConvert.SerializeObject(@event.Params, JsonProtocolSerialization.Settings)}");
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

  private static string GetPresentableSessionId(string? sessionId) => sessionId?.ToLowerInvariant().Substring(0, 5) ?? "<main>";
}