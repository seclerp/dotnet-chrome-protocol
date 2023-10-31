using System.Text;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace ChromeProtocol.Tools.Logging;

public class SpectreLogger : ILogger
{
  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
  {
    var sb = new StringBuilder();
    sb.Append(GetLogLevelPresentable(logLevel));
    sb.Append(state);
    if (exception is not null)
    {

    }
    AnsiConsole.MarkupLine(sb.ToString());
  }

  public bool IsEnabled(LogLevel logLevel) => true;

  public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

  private static string GetLogLevelPresentable(LogLevel logLevel) =>
    logLevel switch
    {
      LogLevel.Trace => $"[grey]{logLevel.ToString()}[/]",
      LogLevel.Debug => $"[grey]{logLevel.ToString()}[/]",
      LogLevel.Information => $"[white]{logLevel.ToString()}[/]",
      LogLevel.Warning => $"[yellow]{logLevel.ToString()}[/]",
      LogLevel.Error => $"[red]{logLevel.ToString()}[/]",
      LogLevel.Critical => $"[red bold]{logLevel.ToString()}[/]",
      LogLevel.None => logLevel.ToString(),
      _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
    };
}
