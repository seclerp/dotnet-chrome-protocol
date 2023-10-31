using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpMethodParameterBuilder : CsharpCodeBuilder<CsharpMethodParameter>
{
  public CsharpMethodParameterBuilder(string name, CsharpTypeInfo type)
  {
    Node.Name = name;
    Node.Type = type;
  }

  public CsharpMethodParameterBuilder DefaultValue(string rawValue)
  {
    Node.DefaultValue = rawValue;

    return this;
  }

  public CsharpMethodParameterBuilder Attribute(CsharpTypeInfo type, Action<CsharpAttributeDeclBuilder> configuration = null)
  {
    var attributeBuilder = new CsharpAttributeDeclBuilder(type);
    configuration?.Invoke(attributeBuilder);

    Node.Attributes.Add(attributeBuilder.Build());

    return this;
  }

  public CsharpMethodParameterBuilder ExtensionMethodTarget(bool isExtensionMethodTarget)
  {
    Node.IsExtensionMethodTarget = isExtensionMethodTarget;

    return this;
  }
}
