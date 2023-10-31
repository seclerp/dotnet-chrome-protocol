namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class CsharpPropertyDecl : CsharpFieldDecl
{
  public bool HasGetter { get; set; } = true;

  public bool HasSetter { get; set; } = true;

  public override void Emit(SourceCodeEmitter emitter, EmissionState state)
  {
    var mergedModifiers = string.Join(string.Empty, Modifiers.Select(m => $"{m} "));

    foreach (var attribute in Attributes)
    {
      attribute.Emit(emitter, state);
    }

    emitter.EmitIdented($"{mergedModifiers}{Type.FullName} {Name}", ref state);

    if (HasGetter || HasSetter)
    {
      emitter.EmitRaw(" {");

      if (HasGetter)
      {
        emitter.EmitRaw(" get;");
      }

      if (HasSetter)
      {
        emitter.EmitRaw(" set;");
      }

      emitter.EmitRaw(" }");
    }

    if (DefaultValue != null)
    {
      emitter.EmitRaw($" = {DefaultValue};");
    }

    emitter.EmitRaw("\n");
  }
}
