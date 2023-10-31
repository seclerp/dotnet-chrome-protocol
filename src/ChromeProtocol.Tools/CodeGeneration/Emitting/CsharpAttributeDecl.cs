namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class CsharpAttributeDecl : IEmittable
{
  public CsharpTypeInfo Type { get; set; }

  /// <example>[property: SomeAttribute]</example>
  public string? Target { get; set; }

  public ICollection<string> RawArguments { get; set; } = new List<string>();

  public void Emit(SourceCodeEmitter emitter, EmissionState state)
  {
    emitter.EmitIdented("[", ref state);
    if (Target is not null)
    {
      emitter.EmitRaw(Target);
      emitter.EmitRaw(": ");
    }
    emitter.EmitRaw(Type.FullName);
    if (RawArguments.Count > 0)
    {
      emitter.EmitRaw("(");
      emitter.EmitRaw(string.Join(", ", RawArguments));
      emitter.EmitRaw(")");
    }

    emitter.EmitRaw("]\n");
  }
}
