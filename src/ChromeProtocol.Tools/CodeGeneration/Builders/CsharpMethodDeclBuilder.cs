using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpMethodDeclBuilder : CsharpCodeBuilder<CsharpMethodDecl>
{
  public CsharpMethodDeclBuilder(string name)
  {
    Node.Name = name;
  }

  public CsharpMethodDeclBuilder Modifiers(params string[] modifiers)
  {
    foreach (var modifier in modifiers)
    {
      Node.Modifiers.Add(modifier);
    }

    return this;
  }

  public CsharpMethodDeclBuilder XmlComment(Action<CsharpXmlCommentBuilder>? configuration = null)
  {
    var commentBuilder = new CsharpXmlCommentBuilder();
    configuration?.Invoke(commentBuilder);
    Node.Comment = commentBuilder.Build();

    return this;
  }

  public CsharpMethodDeclBuilder Attribute(CsharpTypeInfo type, Action<CsharpAttributeDeclBuilder>? configuration = null)
  {
    var attributeBuilder = new CsharpAttributeDeclBuilder(type);
    configuration?.Invoke(attributeBuilder);

    Node.Attributes.Add(attributeBuilder.Build());

    return this;
  }

  public CsharpMethodDeclBuilder ReturnType(CsharpTypeInfo typeInfo)
  {
    Node.ReturnType = typeInfo;

    return this;
  }

  public CsharpMethodDeclBuilder Parameters(Action<CsharpParametersListBuilder>? configuration = null)
  {
    var paramsBuilder = new CsharpParametersListBuilder();
    configuration?.Invoke(paramsBuilder);

    foreach (var parameter in paramsBuilder.Build())
    {
      Node.Parameters.Add(parameter);
    }

    return this;
  }

  public CsharpMethodDeclBuilder Code(params string[] lines)
  {
    foreach (var line in lines)
    {
      Node.Lines.Add(line);
    }

    return this;
  }
}
