using System.Net.WebSockets;
using Microsoft.Extensions.Logging;

namespace ChromeProtocol.Runtime.Messaging.WebSockets;

public class DefaultProtocolClient : WebSocketProtocolClient<ClientWebSocket>
{
  private readonly ILogger _logger;

  public DefaultProtocolClient(Uri wsUri, ILogger logger)
    : base(new ClientWebSocket(), wsUri, logger)
  {
    _logger = logger;
  }

  protected override async Task InitializeConnectionAsync(ClientWebSocket nativeClient, Uri wsUri, CancellationToken token)
  {
    try
    {
      await nativeClient.ConnectAsync(wsUri, token).ConfigureAwait(false);
    }
    catch (WebSocketException ex) when (ex.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
    {
      _logger.LogError(ex, "Chrome has aborted the WebSocket connection during connection establishing.");
    }
  }
}
