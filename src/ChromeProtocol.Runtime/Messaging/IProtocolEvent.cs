namespace ChromeProtocol.Runtime.Messaging;

public interface IProtocolEvent : IProtocolMessage
{
  string Method { get; }
}
