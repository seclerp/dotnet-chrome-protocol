using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace JetBrains.Wasm.Debugger.ChromeProtocol.Tests.Extensions.Logging;

public class XunitLoggerProvider : ILoggerProvider
{
  private readonly ITestOutputHelper _testOutputHelper;
  private readonly string? _prefix;

  public XunitLoggerProvider(ITestOutputHelper testOutputHelper, string? prefix = null)
  {
    _testOutputHelper = testOutputHelper;
    _prefix = prefix;
  }

  public ILogger CreateLogger(string categoryName)
  {
    return new XunitLogger(_testOutputHelper, _prefix);
  }

  public void Dispose()
  {
  }
}
