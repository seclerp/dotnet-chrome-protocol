using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public abstract class CsharpTypeDeclBuilder<TCsharpTypeDeclBuilder, TCsharpTypeDecl> : CsharpCodeBuilder<TCsharpTypeDecl>
  where TCsharpTypeDeclBuilder : CsharpTypeDeclBuilder<TCsharpTypeDeclBuilder, TCsharpTypeDecl>
  where TCsharpTypeDecl : CsharpTypeDecl, new()
{
  protected CsharpTypeDeclBuilder(string name)
  {
    Node.Name = name;
  }

  public TCsharpTypeDeclBuilder Modifiers(params string[] modifiers)
  {
    foreach (var modifier in modifiers)
    {
      Node.Modifiers.Add(modifier);
    }

    return (TCsharpTypeDeclBuilder)this;
  }

  public TCsharpTypeDeclBuilder Inherit(CsharpTypeInfo typeInfo, Action<CsharpArgumentListBuilder>? ctorArguments = default)
  {
    var methodArgsBuilder = new CsharpArgumentListBuilder();
    ctorArguments?.Invoke(methodArgsBuilder);
    var inheritance = new CsharpInheritance(typeInfo);
    if (ctorArguments is not null)
    {
      inheritance.Arguments = methodArgsBuilder.Build();
    }
    Node.BaseTypes.Add(inheritance);

    return (TCsharpTypeDeclBuilder)this;
  }

  public TCsharpTypeDeclBuilder Property(string name, CsharpTypeInfo typeInfo, Action<CsharpPropertyDeclBuilder>? configuration = null, bool hasGetter = true, bool hasSetter = true)
  {
    var propertyBuilder = new CsharpPropertyDeclBuilder(name, typeInfo, hasGetter, hasSetter);
    configuration?.Invoke(propertyBuilder);

    Node.Members.Add(propertyBuilder.Build());

    return (TCsharpTypeDeclBuilder)this;
  }

  public TCsharpTypeDeclBuilder Method(string name, Action<CsharpMethodDeclBuilder>? configuration = null)
  {
    var methodBuilder = new CsharpMethodDeclBuilder(name);
    configuration?.Invoke(methodBuilder);

    Node.Members.Add(methodBuilder.Build());

    return (TCsharpTypeDeclBuilder)this;
  }

  public TCsharpTypeDeclBuilder Attribute(CsharpTypeInfo type, Action<CsharpAttributeDeclBuilder>? configuration = null)
  {
    var attributeBuilder = new CsharpAttributeDeclBuilder(type);
    configuration?.Invoke(attributeBuilder);

    Node.Attributes.Add(attributeBuilder.Build());

    return (TCsharpTypeDeclBuilder)this;
  }

  public TCsharpTypeDeclBuilder Comment(string text)
  {
    Node.Comment = new CsharpComment { Text = text };

    return (TCsharpTypeDeclBuilder)this;
  }

  public TCsharpTypeDeclBuilder XmlComment(Action<CsharpXmlCommentBuilder>? configuration = null)
  {
    var commentBuilder = new CsharpXmlCommentBuilder();
    configuration?.Invoke(commentBuilder);
    Node.Comment = commentBuilder.Build();

    return (TCsharpTypeDeclBuilder)this;
  }

  public TCsharpTypeDeclBuilder Class(string name, Action<CsharpClassDeclBuilder>? configuration = null)
  {
    var classBuilder = new CsharpClassDeclBuilder(name);
    configuration?.Invoke(classBuilder);

    Node.Members.Add(classBuilder.Build());

    return (TCsharpTypeDeclBuilder)this;
  }

  public TCsharpTypeDeclBuilder Record(string name, Action<CsharpRecordDeclBuilder>? configuration = null)
  {
    var recordBuilder = new CsharpRecordDeclBuilder(name);
    configuration?.Invoke(recordBuilder);

    Node.Members.Add(recordBuilder.Build());

    return (TCsharpTypeDeclBuilder)this;
  }
}
