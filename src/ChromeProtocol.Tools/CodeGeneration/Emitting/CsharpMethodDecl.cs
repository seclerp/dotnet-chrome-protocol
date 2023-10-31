namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class CsharpMethodDecl : ICsharpTypeMember
{
  public ICollection<string> Modifiers { get; set; } = new LinkedList<string>();

  public CsharpTypeInfo ReturnType { get; set; } = CsharpTypeInfo.FromFullyQualifiedName("System.Void");

  public string Name { get; set; }

  public ICollection<CsharpMethodParameter> Parameters { get; set; } = new LinkedList<CsharpMethodParameter>();

  public ICollection<string> Lines { get; set; } = new LinkedList<string>();

  public ICollection<CsharpAttributeDecl> Attributes { get; set; } = new LinkedList<CsharpAttributeDecl>();

  public CsharpComment? Comment { get; set; }

  public void Emit(SourceCodeEmitter emitter, EmissionState state)
  {
    var mergedModifiers = string.Join("", Modifiers.Select(m => $"{m} "));
    var parameters = string.Join(", ", Parameters.Select(p =>
    {
      var prefix = p.IsExtensionMethodTarget ? "this " : string.Empty;
      var defaultValue = p.DefaultValue is not null ? $" = {p.DefaultValue}" : string.Empty;
      return $"{prefix}{p.Type.FullName} {p.Name}{defaultValue}";
    }));

    var methodSignature = ReturnType != null
      ? $"{mergedModifiers}{ReturnType.FullName} {Name}({parameters})"
      : $"{mergedModifiers}{Name}({parameters})";

    Comment?.Emit(emitter, state);
    foreach (var attribute in Attributes)
    {
      attribute.Emit(emitter, state);
    }
    emitter.EmitIdented(methodSignature, ref state);

    if (Lines.Any())
    {
      emitter.EmitEmptyLine(ref state);
      emitter.EmitLineIdented("{", ref state);

      var methodBodyState = state.WithIncreasedLevel();
      foreach (var line in Lines)
      {
        emitter.EmitLineIdented(line, ref methodBodyState);
      }

      emitter.EmitLineIdented("}", ref state);
    }
    else
    {
      emitter.EmitLineIdented(";", ref state);
    }
  }
}
