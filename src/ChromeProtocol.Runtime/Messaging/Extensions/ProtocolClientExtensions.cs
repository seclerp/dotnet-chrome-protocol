using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Messaging.Extensions;

public static class ProtocolClientExtensions
{
  public static IDisposable SubscribeOnceAsync<TEvent>(this IProtocolClient client, AsyncDomainEventHandler<TEvent> handler)
    where TEvent : IEvent
  {
    var fired = 0;
    IDisposable? subscription = null;
    subscription = client.SubscribeAsync<TEvent>(async @event =>
    {
      if (Interlocked.CompareExchange(ref fired, 1, 0) != 0)
        return;

      await handler.Invoke(@event).ConfigureAwait(false);

      subscription?.Dispose();
    });

    return subscription;
  }

  public static IDisposable SubscribeOnceSync<TEvent>(this IProtocolClient client, SyncDomainEventHandler<TEvent> handler)
    where TEvent : IEvent
  {
    var fired = 0;
    IDisposable? subscription = null;
    subscription = client.SubscribeSync<TEvent>(@event =>
    {
      if (Interlocked.CompareExchange(ref fired, 1, 0) != 0)
        return;

      handler.Invoke(@event);

      subscription?.Dispose();
    });

    return subscription;
  }

  public static IDisposable SubscribeOnceAsync<TEvent>(this IScopedProtocolClient client, AsyncDomainEventHandler<TEvent> handler)
    where TEvent : IEvent
  {
    var fired = 0;
    IDisposable? subscription = null;
    subscription = client.SubscribeAsync<TEvent>(async @event =>
    {
      if (Interlocked.CompareExchange(ref fired, 1, 0) != 0)
        return;

      await handler.Invoke(@event).ConfigureAwait(false);

      subscription?.Dispose();
    });

    return subscription;
  }

  public static IDisposable SubscribeOnceSync<TEvent>(this IScopedProtocolClient client, SyncDomainEventHandler<TEvent> handler)
    where TEvent : IEvent
  {
    var fired = 0;
    IDisposable? subscription = null;
    subscription = client.SubscribeSync<TEvent>(@event =>
    {
      if (Interlocked.CompareExchange(ref fired, 1, 0) != 0)
        return;

      handler.Invoke(@event);

      subscription?.Dispose();
    });

    return subscription;
  }
}
