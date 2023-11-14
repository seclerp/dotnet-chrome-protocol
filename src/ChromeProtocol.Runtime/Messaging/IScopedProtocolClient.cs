using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Messaging;

public interface IScopedProtocolClient : IDisposable
{
  public string SessionId { get; }

  [Obsolete("Use SubscribeSync or SubscribeAsync instead.")]
  void ListenEvent<TEvent>(AsyncDomainEventHandler<TEvent> handler)
    where TEvent : IEvent;

  IDisposable SubscribeAsync<TEvent>(AsyncDomainEventHandler<TEvent> handler)
    where TEvent : IEvent;

  IDisposable SubscribeSync<TEvent>(SyncDomainEventHandler<TEvent> handler)
    where TEvent : IEvent;

  Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken? token = default)
    where TResponse : IType;

  Task FireCommandAsync(ICommand command, CancellationToken token = default);
}
