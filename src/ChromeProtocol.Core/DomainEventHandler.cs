namespace ChromeProtocol.Core;

/// <summary>
/// Synchronous CDP event handler delegate.
/// </summary>
/// <typeparam name="TEvent">Type of the event from CDP.</typeparam>
public delegate void SyncDomainEventHandler<in TEvent>(TEvent @event)
  where TEvent : IEvent;

/// <summary>
/// Asynchronous CDP event handler delegate.
/// </summary>
/// <typeparam name="TEvent">Type of the event from CDP.</typeparam>
public delegate Task AsyncDomainEventHandler<in TEvent>(TEvent @event)
  where TEvent : IEvent;
