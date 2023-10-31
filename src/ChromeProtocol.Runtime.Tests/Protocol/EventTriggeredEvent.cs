using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Tests.Protocol;

[MethodName("EventTriggered")]
public record EventTriggeredEvent : IEvent
{
}
