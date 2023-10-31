using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpFieldDeclBuilder : CsharpCodeBuilder<CsharpFieldDecl>
{
  public CsharpFieldDeclBuilder(string name, CsharpTypeInfo typeInfo)
  {
    Node.Name = name;
    Node.Type = typeInfo;
  }

  public CsharpFieldDeclBuilder Modifiers(params string[] modifiers)
  {
    foreach (var modifier in modifiers)
    {
      Node.Modifiers.Add(modifier);
    }

    return this;
  }

  public CsharpFieldDeclBuilder DefaultValue(string value)
  {
    Node.DefaultValue = value;

    return this;
  }
}
