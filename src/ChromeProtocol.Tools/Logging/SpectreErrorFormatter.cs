using System.Text;
using ChromeProtocol.Tools.CodeGeneration.Pipeline;
using Json.Schema;

namespace ChromeProtocol.Tools.Logging;

public class SpectreErrorFormatter : IErrorFormatter
{
  public string Format(string fileName, EvaluationResults errors)
  {
    var position = $" -> {errors.InstanceLocation}";
    var builder = new StringBuilder();

    if (errors.Errors is not null)
    {
      foreach (var (_, error) in errors.Errors)
      {
        builder.Append("[underline orange3]");
        {
          builder.Append(EscapeMarkup(Path.GetFileName(fileName)));
          builder.Append(EscapeMarkup(position));
        }
        builder.Append("[/]");
        builder.Append("[bold orange3]");
        {
          builder.Append(": ");
          builder.Append(EscapeMarkup(error));
        }
        builder.Append("[/]");
      }
    }

    return builder.ToString();
  }

  public string Format(CodeGenerationError error)
  {
    var builder = new StringBuilder();
    builder.Append("[underline orange3]");
    {
      builder.Append(EscapeMarkup(Path.GetFileName(error.FileName)));
    }
    builder.Append("[/]");
    builder.Append("[bold orange3]");
    {
      builder.Append(": ");
      builder.Append(EscapeMarkup(error.Message));
    }
    builder.Append("[/]");
    return builder.ToString();
  }

  /// <summary>
  /// Spectre.Console recognizes `[` and `]` as BBCode tags, so we should escape them properly.
  /// </summary>
  /// <returns></returns>
  private static string EscapeMarkup(string input) =>
    input.Replace("[", "[[").Replace("]", "]]");
}
