namespace ChromeProtocol.Runtime.Messaging.Logging;

/// <summary>
/// A record that represents configuration options for <see cref="ProtocolClientLogger"/>.
/// </summary>
/// <param name="MaxMessageLengthChars">Maximal length of the protocol client message to be logged.
/// If a message exceeds this limit, it will be truncated.
/// If you want to disable truncating, use -1 as provided value.</param>
public record ProtocolClientLoggerOptions(int MaxMessageLengthChars)
{
  /// <summary>
  /// Default protocol client options.
  /// </summary>
  public static readonly ProtocolClientLoggerOptions Default = new ProtocolClientLoggerOptions(1_000);
}
