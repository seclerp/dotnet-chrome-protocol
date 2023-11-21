namespace ChromeProtocol.Runtime.Messaging.Logging;

public record ProtocolClientLoggerOptions(int MaxMessageLengthChars)
{
  public static readonly ProtocolClientLoggerOptions Default = new ProtocolClientLoggerOptions(1_000);
}
