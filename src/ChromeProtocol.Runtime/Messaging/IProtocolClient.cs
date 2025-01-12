using System.Text.Json.Nodes;
using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Messaging;

public interface IProtocolClient : IDisposable
{
  event EventHandler OnConnected;
  event EventHandler OnDisconnected;
  event EventHandler<ProtocolRequest<ICommand>> OnRequestSent;
  event EventHandler<ProtocolResponse<JsonObject>> OnResponseReceived;
  event EventHandler<ProtocolEvent<JsonObject>> OnEventReceived;

  Task ConnectAsync(CancellationToken token = default);

  Task DisconnectAsync(CancellationToken token = default);

  [Obsolete("Use SubscribeSync or SubscribeAsync instead.")]
  void ListenEvent<TEvent>(AsyncDomainEventHandler<TEvent> handler)
    where TEvent : IEvent;

  IDisposable SubscribeAsync<TEvent>(AsyncDomainEventHandler<TEvent> handler, string? sessionId = default)
    where TEvent : IEvent;

  IDisposable SubscribeSync<TEvent>(SyncDomainEventHandler<TEvent> handler, string? sessionId = default)
    where TEvent : IEvent;

  Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, string? sessionId = default, CancellationToken? token = default)
    where TResponse : IType;

  Task FireCommandAsync(ICommand command, string? sessionId = default, CancellationToken token = default);

  IScopedProtocolClient CreateScoped(string sessionId);
}
