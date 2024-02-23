using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace ChromeProtocol.Runtime.Tests.Protocol.TestServer;

public static class TestServerFactory
{
  public static WebApplicationFactory<TProgram> CreateTestServer<TProgram>() where TProgram : class
  {
    return new WebApplicationFactory<TProgram>()
      .WithWebHostBuilder(builder => builder
        .UseSolutionRelativeContentRoot(Directory.GetCurrentDirectory()));
  }
}
