namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class CsharpFieldDecl : ICsharpTypeMember
{
  public ICollection<string> Modifiers { get; set; } = new LinkedList<string>();

  public CsharpTypeInfo Type { get; set; }

  public string Name { get; set; }

  public string DefaultValue { get; set; } = null;

  public ICollection<CsharpAttributeDecl> Attributes { get; set; } = new LinkedList<CsharpAttributeDecl>();

  public CsharpComment? Comment { get; set; }

  public virtual void Emit(SourceCodeEmitter emitter, EmissionState state)
  {
    var mergedModifiers = string.Join(string.Empty, Modifiers.Select(m => $"{m} "));

    Comment?.Emit(emitter, state);
    foreach (var attribute in Attributes)
    {
      attribute.Emit(emitter, state);
    }
    emitter.EmitIdented($"{mergedModifiers}{Type.FullName} {Name}", ref state);

    if (DefaultValue != null)
    {
      emitter.EmitRaw($" = {DefaultValue}");
    }

    emitter.EmitRaw(";\n");
  }
}
