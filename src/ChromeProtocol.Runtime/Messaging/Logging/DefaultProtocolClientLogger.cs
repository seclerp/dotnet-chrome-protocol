using Microsoft.Extensions.Logging;

namespace ChromeProtocol.Runtime.Messaging.Logging;

public class DefaultProtocolClientLogger : ProtocolClientLogger
{
  private readonly ILogger _logger;

  public DefaultProtocolClientLogger(IProtocolClient client, ILogger logger) : this(client, logger, ProtocolClientLoggerOptions.Default)
  {
  }

  public DefaultProtocolClientLogger(IProtocolClient client, ILogger logger, ProtocolClientLoggerOptions options) : base(client, options)
  {
    _logger = logger;
  }

  protected override void LogConnected(string message) => _logger.LogInformation(message);
  protected override void LogDisconnected(string message) => _logger.LogInformation(message);
  protected override void LogOutgoingRequest(string message) => _logger.LogInformation(message);
  protected override void LogIncomingResponse(string message) => _logger.LogInformation(message);
  protected override void LogIncomingUnknownResponse(string message) => _logger.LogWarning(message);
  protected override void LogIncomingError(string message) => _logger.LogError(message);
  protected override void LogIncomingEvent(string message) => _logger.LogInformation(message);
}
