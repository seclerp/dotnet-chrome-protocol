namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class CsharpComment
{
  public string? Text { get; set;  }

  public virtual void Emit(SourceCodeEmitter emitter, EmissionState state)
  {
    if (!string.IsNullOrEmpty(Text))
      foreach (var line in Text.Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries))
        emitter.EmitLineIdented($"// {line}", ref state);
  }
}
