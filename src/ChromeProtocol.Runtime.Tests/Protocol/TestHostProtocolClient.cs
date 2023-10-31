using System.Net.WebSockets;
using ChromeProtocol.Runtime.Messaging.WebSockets;
using Microsoft.AspNetCore.TestHost;

namespace ChromeProtocol.Runtime.Tests.Protocol;

public class TestHostProtocolClient : WebSocketProtocolClient<WebSocket>
{
  private TestHostProtocolClient(WebSocket nativeClient, Uri wsUri, ILogger logger)
    : base(nativeClient, wsUri, logger)
  {
  }

  public static async Task<TestHostProtocolClient> CreateAsync(WebSocketClient testClient, Uri wsUri, ILogger logger, CancellationToken token = default)
  {
    var nativeClient = await testClient.ConnectAsync(wsUri, token).ConfigureAwait(false);

    return new TestHostProtocolClient(nativeClient, wsUri, logger);
  }
}