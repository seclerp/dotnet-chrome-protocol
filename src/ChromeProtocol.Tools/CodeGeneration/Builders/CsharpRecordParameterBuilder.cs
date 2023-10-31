using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpRecordParameterBuilder : CsharpCodeBuilder<CsharpRecordParameter>
{
  public CsharpRecordParameterBuilder(string name, CsharpTypeInfo type)
  {
    Node.Name = name;
    Node.Type = type;
  }

  public CsharpRecordParameterBuilder DefaultValue(string rawValue)
  {
    Node.DefaultValue = rawValue;

    return this;
  }

  public CsharpRecordParameterBuilder Attribute(CsharpTypeInfo type, Action<CsharpAttributeDeclBuilder> configuration = null)
  {
    var attributeBuilder = new CsharpAttributeDeclBuilder(type);
    configuration?.Invoke(attributeBuilder);

    Node.Attributes.Add(attributeBuilder.Build());

    return this;
  }
}
