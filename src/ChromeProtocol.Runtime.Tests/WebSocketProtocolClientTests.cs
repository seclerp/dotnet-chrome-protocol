using ChromeProtocol.Runtime.Messaging;
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

  public WebSocketProtocolClientTests(TestServerFixture serverFixture, ITestOutputHelper testOutputHelper)
  {
    _serverFactory = serverFixture.ServerFactory;
    _clientLogger = new XunitLogger(testOutputHelper, "Client");
  }

  public async Task InitializeAsync()
  {
    var nativeClient = _serverFactory.Server.CreateWebSocketClient();
    _protocolClient = await TestHostProtocolClient.CreateAsync(nativeClient, _serverFactory.Server.BaseAddress, _clientLogger);
    // await _serverFactory.Server.Host.StartAsync();
    _clientLogger.LogInformation("Connecting to the test WebSocket server...");
    await _protocolClient.ConnectAsync().ConfigureAwait(false);
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
      await _protocolClient.SendCommandAsync(new AlwaysErrorCommand()).ConfigureAwait(false);
    }
    catch (ProtocolErrorException e)
    {
      Assert.Equal(-1, e.Info.Code);
      Assert.Equal("I'm always failing", e.Info.Message);
    }
  }

  [Fact]
  public async Task FireCommandAsync_TriggerEvent_ShouldSendEvent()
  {
    var awaiter = new ManualResetEvent(false);

    _protocolClient.ListenEvent((EventTriggeredEvent _) =>
    {
      awaiter.Set();
      return Task.CompletedTask;
    });
    await _protocolClient.FireCommandAsync(new TriggerEventCommand()).ConfigureAwait(false);
    var signalled = awaiter.WaitOne(TimeSpan.FromSeconds(10));

    Assert.True(signalled);
  }

  public async Task DisposeAsync()
  {
    _clientLogger.LogInformation("Shutdown");
    _protocolClient.Dispose();
  }
}
