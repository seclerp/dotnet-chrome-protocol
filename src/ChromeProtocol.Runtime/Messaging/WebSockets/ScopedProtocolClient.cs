using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Messaging.WebSockets;

public class ScopedProtocolClient : IScopedProtocolClient
{
  private readonly IProtocolClient _mainClient;
  public string SessionId { get; }

  public ScopedProtocolClient(IProtocolClient mainClient, string sessionId)
  {
    _mainClient = mainClient;
    SessionId = sessionId;
  }

  // TODO: Support session-specific events
  public void ListenEvent<TEvent>(AsyncDomainEventHandler<TEvent> handler) where TEvent : IEvent =>
    SubscribeAsync(handler);

  public IDisposable SubscribeAsync<TEvent>(AsyncDomainEventHandler<TEvent> handler) where TEvent : IEvent =>
    _mainClient.SubscribeAsync(handler);

  public IDisposable SubscribeSync<TEvent>(SyncDomainEventHandler<TEvent> handler) where TEvent : IEvent =>
    _mainClient.SubscribeSync(handler);

  public Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken? token = default)
    where TResponse : IType => _mainClient.SendCommandAsync(command, SessionId, token);

  public Task FireCommandAsync(ICommand command, CancellationToken token = default) =>
    _mainClient.FireCommandAsync(command, SessionId, token);

  public void Dispose()
  {
  }
}
