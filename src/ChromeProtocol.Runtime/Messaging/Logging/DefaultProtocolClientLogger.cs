using Microsoft.Extensions.Logging;

namespace ChromeProtocol.Runtime.Messaging.Logging;

public class DefaultProtocolClientLogger : ProtocolClientLogger
{
  private readonly ILogger _logger;

  public DefaultProtocolClientLogger(IProtocolClient client, ILogger logger) : base(client)
  {
    _logger = logger;
  }

  public override void LogConnected(string message) => _logger.LogInformation(message);
  public override void LogDisconnected(string message) => _logger.LogInformation(message);
  public override void LogOutgoingRequest(string message) => _logger.LogInformation(message);
  public override void LogIncomingResponse(string message) => _logger.LogInformation(message);
  public override void LogIncomingUnknownResponse(string message) => _logger.LogWarning(message);
  public override void LogIncomingError(string message) => _logger.LogError(message);
  public override void LogIncomingEvent(string message) => _logger.LogInformation(message);
}