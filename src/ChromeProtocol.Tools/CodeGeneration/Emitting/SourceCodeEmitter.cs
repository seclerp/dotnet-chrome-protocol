using System.Text;

namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class SourceCodeEmitter
{
  private readonly int _indentationSize;
  private readonly char _indentationSymbol;

  public SourceCodeEmitter(int indentationSize = 2, char indentationSymbol = ' ')
  {
    _indentationSize = indentationSize;
    _indentationSymbol = indentationSymbol;
  }

  private StringBuilder _stringBuilder = new StringBuilder();

  public void EmitRaw(string sourceCode)
  {
    _stringBuilder.Append(sourceCode);
  }

  public void EmitIdented(string sourceCode, ref EmissionState state)
  {
    EmitRaw($"{new string(_indentationSymbol, state.Level * _indentationSize)}{sourceCode}");
  }

  public void EmitLineIdented(string sourceCode, ref EmissionState state)
  {
    EmitIdented(sourceCode, ref state);
    _stringBuilder.AppendLine();
  }

  public void EmitEmptyLine(ref EmissionState state)
  {
    EmitLineIdented(string.Empty, ref state);
  }

  public string Finish()
  {
    return _stringBuilder.ToString();
  }
}
