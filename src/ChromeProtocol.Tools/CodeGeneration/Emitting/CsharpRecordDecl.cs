namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class CsharpRecordDecl : CsharpTypeDecl
{
  public CsharpRecordDecl()
    : base("record")
  {
  }

  /// <summary>
  /// Parameters that should be presented in primary constructor
  /// </summary>
  public ICollection<CsharpRecordParameter> Parameters { get; } = new LinkedList<CsharpRecordParameter>();

  public override void Emit(SourceCodeEmitter emitter, EmissionState state)
  {
    var mergedModifiers = string.Join(string.Empty, Modifiers.Select(m => $"{m} "));
    Comment?.Emit(emitter, state);
    foreach (var attribute in Attributes)
    {
      attribute.Emit(emitter, state);
    }
    emitter.EmitIdented($"{mergedModifiers}{TypeKeyword} {Name}", ref state);

    if (Parameters.Any())
    {
      emitter.EmitRaw("(\n");
      var parameterLevelState = state.WithIncreasedLevel();
      foreach (var (parameter, index) in Parameters.Select((param, i) => (param, i)))
      {
        var separator = index < Parameters.Count - 1 ? "," : string.Empty;
        var defaultValue = parameter.DefaultValue is not null ? $" = {parameter.DefaultValue}" : string.Empty;
        foreach (var attribute in parameter.Attributes)
        {
          attribute.Emit(emitter, parameterLevelState);
        }

        emitter.EmitLineIdented($"{parameter.Type.FullName} {parameter.Name}{defaultValue}{separator}", ref parameterLevelState);
      }
      emitter.EmitIdented(")", ref state);
    }
    else
    {
      emitter.EmitRaw("()");
    }

    if (BaseTypes.Count > 0)
    {
      emitter.EmitRaw(" : ");
      var baseTypesSeparated = string.Join(", ", BaseTypes.Select(GenerateInheritance));
      emitter.EmitRaw(baseTypesSeparated);
      emitter.EmitRaw("\n");
    }
    else
    {
      emitter.EmitRaw("\n");
    }

    emitter.EmitLineIdented("{", ref state);
    foreach (var member in Members)
    {
      member.Emit(emitter, state.WithIncreasedLevel());
    }
    emitter.EmitLineIdented("}", ref state);
  }
}
