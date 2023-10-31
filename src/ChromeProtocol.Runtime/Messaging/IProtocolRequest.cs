namespace ChromeProtocol.Runtime.Messaging;

public interface IProtocolRequest : IProtocolMessage
{
  int Id { get; }
  string Method { get; }
}
