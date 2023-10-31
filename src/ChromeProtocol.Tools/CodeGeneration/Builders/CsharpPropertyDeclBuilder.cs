using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpPropertyDeclBuilder : CsharpCodeBuilder<CsharpPropertyDecl>
{
  public CsharpPropertyDeclBuilder(string name, CsharpTypeInfo typeInfo, bool hasGetter = true, bool hasSetter = true)
  {
    Node.Name = name;
    Node.Type = typeInfo;
    Node.HasGetter = hasGetter;
    Node.HasSetter = hasSetter;
  }

  public CsharpPropertyDeclBuilder Modifiers(params string[] modifiers)
  {
    foreach (var modifier in modifiers)
    {
      Node.Modifiers.Add(modifier);
    }

    return this;
  }

  public CsharpPropertyDeclBuilder DefaultValue(string value)
  {
    Node.DefaultValue = value;

    return this;
  }

  public CsharpPropertyDeclBuilder Attribute(CsharpTypeInfo type, Action<CsharpAttributeDeclBuilder> configuration = null)
  {
    var attributeBuilder = new CsharpAttributeDeclBuilder(type);
    configuration?.Invoke(attributeBuilder);

    Node.Attributes.Add(attributeBuilder.Build());

    return this;
  }
}
