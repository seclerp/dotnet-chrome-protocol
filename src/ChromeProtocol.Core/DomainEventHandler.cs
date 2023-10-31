namespace ChromeProtocol.Core;

public delegate Task DomainEventHandler<in TEvent>(TEvent @event)
  where TEvent : IEvent;
