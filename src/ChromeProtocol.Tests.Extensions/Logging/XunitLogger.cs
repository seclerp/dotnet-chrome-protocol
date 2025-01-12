using System.Text;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ChromeProtocol.Tests.Extensions.Logging;

/// <summary>
/// A <see cref="ILogger"/> implementation that works as an adaptor over <see cref="ITestOutputHelper"/>.
/// </summary>
public sealed class XunitLogger : ILogger, IDisposable
{
  private ITestOutputHelper _output;
  private readonly string? _prefix;

  /// <summary>
  /// Creates an instance of <see cref="XunitLogger"/>.
  /// </summary>
  /// <param name="output"></param>
  /// <param name="prefix"></param>
  public XunitLogger(ITestOutputHelper output, string? prefix = null)
  {
    _output = output;
    _prefix = prefix;
  }

  /// <inheritdoc />
  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
  {
    try
    {
      var builder = new StringBuilder();
      builder.Append($"{logLevel.ToString()} ");

      if (!string.IsNullOrEmpty(_prefix))
      {
        builder.Append(_prefix);
        builder.Append(": ");
      }

      builder.Append(state);

      if (exception != null)
      {
        builder.Append($", exception: {exception}");
      }

      _output.WriteLine(builder.ToString());
    }
    catch (InvalidOperationException)
    {
      // Sometimes test resource cleanup from other threads happening after test lifecycle.
      // That's why here sometimes may happen "System.InvalidOperationException: There is no currently active test.".
    }
  }

  /// <inheritdoc />
  public bool IsEnabled(LogLevel logLevel)
  {
    return true;
  }

  /// <inheritdoc />
  IDisposable ILogger.BeginScope<TState>(TState state)
  {
    return this;
  }

  /// <inheritdoc />
  public void Dispose()
  {
  }
}
