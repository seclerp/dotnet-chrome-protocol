using ChromeProtocol.Runtime.Messaging;
using ChromeProtocol.Runtime.Messaging.Extensions;
using ChromeProtocol.Runtime.Tests.Protocol;
using ChromeProtocol.Runtime.Tests.Protocol.TestServer;
using ChromeProtocol.Tests.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace ChromeProtocol.Runtime.Tests;

public class WebSocketProtocolClientTests : IClassFixture<TestServerFixture>, IAsyncLifetime
{
  private readonly WebApplicationFactory<Protocol.TestServer.Program> _serverFactory;
  private readonly ILogger _clientLogger;
  private IProtocolClient _protocolClient;
  private CancellationTokenSource _tokenSource = new CancellationTokenSource(30_000);
  private CancellationToken Token => _tokenSource.Token;

  public WebSocketProtocolClientTests(TestServerFixture serverFixture, ITestOutputHelper testOutputHelper)
  {
    _serverFactory = serverFixture.ServerFactory;
    _clientLogger = new XunitLogger(testOutputHelper, "Client");
  }

  public async Task InitializeAsync()
  {
    var nativeClient = _serverFactory.Server.CreateWebSocketClient();
    _protocolClient = await TestHostProtocolClient.CreateAsync(nativeClient, new Uri(_serverFactory.Server.BaseAddress, "/ws"), _clientLogger, Token);
    _clientLogger.LogInformation("Connecting to the test WebSocket server...");
    await _protocolClient.ConnectAsync(Token).ConfigureAwait(false);
    _clientLogger.LogInformation("Connected successfully");
  }

  [Fact]
  public async Task SendCommandAsync_Echo_ShouldSucceed()
  {
    var result = await _protocolClient.SendCommandAsync(new EchoCommand("Hello")).ConfigureAwait(false);

    Assert.Equal("Hello", result.Message);
  }

  [Fact]
  public async Task SendCommandAsync_AlwaysError_ShouldFail()
  {
    try
    {
      await _protocolClient.SendCommandAsync(new AlwaysErrorCommand(), token: Token).ConfigureAwait(false);
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
    _serverFactory.Server.CreateWebSocketClient();
    var tcs = new TaskCompletionSource<bool>();
    Token.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);

    var subscription = _protocolClient.SubscribeOnceAsync<EventTriggeredEvent>(async _ =>
    {
      tcs.TrySetResult(true);
    });

    Token.Register(() => subscription.Dispose());

    await _protocolClient.FireCommandAsync(new TriggerEventCommand(), token: Token).ConfigureAwait(false);
    var result = await tcs.Task.ConfigureAwait(false);

    Assert.True(result);
  }

  public async Task DisposeAsync()
  {
    _tokenSource.Cancel();
    _clientLogger.LogInformation("Shutdown");
    _protocolClient.Dispose();
  }
}
