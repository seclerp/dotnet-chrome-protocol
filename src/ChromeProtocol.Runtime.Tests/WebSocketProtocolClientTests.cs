using ChromeProtocol.Runtime.Messaging;
using ChromeProtocol.Runtime.Messaging.Extensions;
using ChromeProtocol.Runtime.Tests.Protocol;
using ChromeProtocol.Runtime.Tests.Protocol.TestServer;
using ChromeProtocol.Tests.Extensions.Logging;
using Microsoft.AspNetCore.TestHost;
using Xunit.Abstractions;

namespace ChromeProtocol.Runtime.Tests;

public class WebSocketProtocolClientTests : IAsyncLifetime
{
  private readonly ILogger _clientLogger;

  public WebSocketProtocolClientTests(ITestOutputHelper testOutputHelper)
  {
    _clientLogger = new XunitLogger(testOutputHelper, "Client");
  }

  [Fact]
  public async Task SendCommandAsync_Echo_ShouldSucceed()
  {
    var tokenSource = new CancellationTokenSource(30_0000);
    await using var factory = TestServerFactory.CreateTestServer<Program>();
    using var client = await CreateServerClient(factory.Server, tokenSource.Token).ConfigureAwait(false);

    var result = await client.SendCommandAsync(new EchoCommand("Hello"), token: tokenSource.Token).ConfigureAwait(false);

    Assert.Equal("Hello", result.Message);
  }

  [Fact]
  public async Task SendCommandAsync_AlwaysError_ShouldFail()
  {
    var tokenSource = new CancellationTokenSource(30_0000);
    await using var factory = TestServerFactory.CreateTestServer<Program>();
    using var client = await CreateServerClient(factory.Server, tokenSource.Token).ConfigureAwait(false);

    try
    {
      await client.SendCommandAsync(new AlwaysErrorCommand(), token: tokenSource.Token).ConfigureAwait(false);
    }
    catch (ProtocolErrorException e)
    {
      Assert.Equal(-1, e.Info.Code);
      Assert.Equal("I'm always failing", e.Info.Message);
    }
  }

  [Fact]
  public async Task FireCommandAsync_TriggerEvent_ShouldSendEventBack()
  {
    var tokenSource = new CancellationTokenSource(30_0000);
    await using var factory = TestServerFactory.CreateTestServer<Program>();
    using var client = await CreateServerClient(factory.Server, tokenSource.Token).ConfigureAwait(false);

    var tcs = new TaskCompletionSource<bool>();
    tokenSource.Token.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);

    var subscription = client.SubscribeOnceAsync<EventTriggeredEvent>(async _ =>
    {
      tcs.TrySetResult(true);
    });

    tokenSource.Token.Register(() => subscription.Dispose());

    await client.FireCommandAsync(new TriggerEventCommand(), token: tokenSource.Token).ConfigureAwait(false);
    var result = await tcs.Task.ConfigureAwait(false);

    Assert.True(result);
  }

  public async Task InitializeAsync()
  {
    _clientLogger.LogInformation("Start");
  }

  public async Task DisposeAsync()
  {
    _clientLogger.LogInformation("Shutdown");
  }

  private async Task<IProtocolClient> CreateServerClient(TestServer server, CancellationToken token = default)
  {
    _clientLogger.LogInformation("Connecting to the test WebSocket server...");
    var nativeClient = server.CreateWebSocketClient();
    var client = await TestHostProtocolClient.CreateAsync(nativeClient, new Uri(server.BaseAddress, "/ws"), _clientLogger, token).ConfigureAwait(false);
    await client.ConnectAsync(token).ConfigureAwait(false);
    _clientLogger.LogInformation("Connected successfully");

    return client;
  }
}
