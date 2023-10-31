using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Messaging;

public interface IScopedProtocolClient : IDisposable
{
  public string SessionId { get; }

  void ListenEvent<TEvent>(DomainEventHandler<TEvent> handler)
    where TEvent : IEvent;

  Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken? token = default)
    where TResponse : IType;

  Task FireCommandAsync(ICommand command, CancellationToken token = default);
}