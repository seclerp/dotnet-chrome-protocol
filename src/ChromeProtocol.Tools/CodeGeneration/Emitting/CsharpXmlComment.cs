namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class CsharpXmlComment : CsharpComment
{
  public override void Emit(SourceCodeEmitter emitter, EmissionState state)
  {
    if (!string.IsNullOrEmpty(Text))
      foreach (var line in Text.Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries))
        emitter.EmitLineIdented($"/// {line}", ref state);
  }
}
