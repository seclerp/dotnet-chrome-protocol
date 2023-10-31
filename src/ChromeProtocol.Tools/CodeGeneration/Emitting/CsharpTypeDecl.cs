using System.Text;

namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public abstract class CsharpTypeDecl : ICsharpTypeMember
{
  protected CsharpTypeDecl(string typeKeyword)
  {
    TypeKeyword = typeKeyword;
  }

  protected string TypeKeyword { get; }

  public ICollection<string> Modifiers { get; set; } = new LinkedList<string>();

  public string Name { get; set; }

  public ICollection<CsharpInheritance> BaseTypes { get; set; } = new LinkedList<CsharpInheritance>();

  public ICollection<ICsharpTypeMember> Members { get; set; } = new LinkedList<ICsharpTypeMember>();

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
    emitter.EmitIdented($"{mergedModifiers}{TypeKeyword} {Name}", ref state);

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

  protected string GenerateInheritance(CsharpInheritance inheritance)
  {
    var sb = new StringBuilder();
    sb.Append(inheritance.Type.FullName);
    if (inheritance.Arguments is not null)
    {
      sb.Append("(");
      var parametersSeparated = string.Join(", ", inheritance.Arguments.Select(p => p.Name));
      sb.Append(parametersSeparated);
      sb.Append(")");
    }

    return sb.ToString();
  }
}
