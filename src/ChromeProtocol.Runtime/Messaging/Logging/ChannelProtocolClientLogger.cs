using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace ChromeProtocol.Runtime.Messaging.Logging;

public class ChannelProtocolClientLogger : ProtocolClientLogger
{
  private readonly ChannelWriter<Payload> _writer;
  public ChannelReader<Payload> Reader { get; }

  public ChannelProtocolClientLogger(IProtocolClient client, Channel<Payload> channel) : this(client, channel, ProtocolClientLoggerOptions.Default)
  {
  }

  public ChannelProtocolClientLogger(IProtocolClient client, Channel<Payload> channel, ProtocolClientLoggerOptions options) : base(client, options)
  {
    _writer = channel.Writer;
    Reader = channel.Reader;
  }

  public override void LogConnected(string message) => _writer.TryWrite(new Payload(message, LogLevel.Information));
  public override void LogDisconnected(string message) => _writer.TryWrite(new Payload(message, LogLevel.Information));
  public override void LogOutgoingRequest(string message) => _writer.TryWrite(new Payload(message, LogLevel.Information));
  public override void LogIncomingResponse(string message) => _writer.TryWrite(new Payload(message, LogLevel.Information));
  public override void LogIncomingUnknownResponse(string message) => _writer.TryWrite(new Payload(message, LogLevel.Warning));
  public override void LogIncomingError(string message) => _writer.TryWrite(new Payload(message, LogLevel.Error));
  public override void LogIncomingEvent(string message) =>  _writer.TryWrite(new Payload(message, LogLevel.Information));

  public record Payload(string Message, LogLevel Level);
}
