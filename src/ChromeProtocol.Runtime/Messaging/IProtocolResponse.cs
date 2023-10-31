namespace ChromeProtocol.Runtime.Messaging;

public interface IProtocolResponse : IProtocolMessage
{
  int Id { get; }
}
