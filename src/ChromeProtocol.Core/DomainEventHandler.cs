namespace ChromeProtocol.Core;

public delegate void SyncDomainEventHandler<in TEvent>(TEvent @event)
  where TEvent : IEvent;

public delegate Task AsyncDomainEventHandler<in TEvent>(TEvent @event)
  where TEvent : IEvent;
