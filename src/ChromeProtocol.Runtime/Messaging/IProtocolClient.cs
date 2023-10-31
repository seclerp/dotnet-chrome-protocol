using ChromeProtocol.Core;
using Newtonsoft.Json.Linq;

namespace ChromeProtocol.Runtime.Messaging;

public interface IProtocolClient : IDisposable
{
  event EventHandler OnConnected;

  event EventHandler OnDisconnected;

  event EventHandler<ProtocolRequest<ICommand>> OnRequestSent;
  event EventHandler<ProtocolResponse<JObject>> OnResponseReceived;
  event EventHandler<ProtocolEvent<JObject>> OnEventReceived;

  Task ConnectAsync(CancellationToken token = default);

  Task DisconnectAsync(CancellationToken token = default);

  void ListenEvent<TEvent>(DomainEventHandler<TEvent> handler)
    where TEvent : IEvent;

  Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, string? sessionId = default, CancellationToken? token = default)
    where TResponse : IType;

  Task FireCommandAsync(ICommand command, string? sessionId = default, CancellationToken token = default);

  IScopedProtocolClient CreateScoped(string sessionId);
}