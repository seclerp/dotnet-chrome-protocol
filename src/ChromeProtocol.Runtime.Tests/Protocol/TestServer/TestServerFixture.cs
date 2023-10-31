using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit.Abstractions;

namespace ChromeProtocol.Runtime.Tests.Protocol.TestServer;

public class TestServerFixture : IClassFixture<ITestOutputHelper>, IDisposable
{
  public WebApplicationFactory<Program> ServerFactory { get; } = new WebApplicationFactory<Program>()
    .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(Directory.GetCurrentDirectory()));

  public void Dispose()
  {
    ServerFactory.Dispose();
  }
}
