using Microsoft.Extensions.Logging;

namespace ChromeProtocol.Usage;

public class SimpleConsoleLogger : ILogger
{
  private readonly string _name;
  private readonly LogLevel _minLogLevel;

  public SimpleConsoleLogger(string name, LogLevel minLogLevel = LogLevel.Information)
  {
    _name = name;
    _minLogLevel = minLogLevel;
  }

  public IDisposable BeginScope<TState>(TState state) => null;

  public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLogLevel;

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
  {
    if (!IsEnabled(logLevel))
    {
      return;
    }

    if (formatter == null)
    {
      throw new ArgumentNullException(nameof(formatter));
    }

    string message = formatter(state, exception);

    if (string.IsNullOrEmpty(message))
    {
      return;
    }

    Console.WriteLine($"{DateTime.Now:o}: [{logLevel}] {_name} - {message}");

    if (exception != null)
    {
      Console.WriteLine(exception);
    }
  }
}
