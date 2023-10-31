using System.Net.WebSockets;
using Microsoft.Extensions.Logging;

namespace ChromeProtocol.Runtime.Messaging.WebSockets;

public class DefaultProtocolClient : WebSocketProtocolClient<ClientWebSocket>
{
  public DefaultProtocolClient(Uri wsUri, ILogger logger)
    : base(new ClientWebSocket(), wsUri, logger)
  {
  }

  protected override async Task InitializeConnectionAsync(ClientWebSocket nativeClient, Uri wsUri, CancellationToken token) =>
    await nativeClient.ConnectAsync(wsUri, token).ConfigureAwait(false);
}