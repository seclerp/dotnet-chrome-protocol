using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpNamespaceDeclBuilder : CsharpCodeBuilder<CsharpNamespaceDecl>
{
  public CsharpNamespaceDeclBuilder(string fullName)
  {
    Node.FullName = fullName;
  }

  public CsharpNamespaceDeclBuilder Class(string name, Action<CsharpClassDeclBuilder> configuration = null)
  {
    var classBuilder = new CsharpClassDeclBuilder(name);
    configuration?.Invoke(classBuilder);

    Node.Classes.Add(classBuilder.Build());

    return this;
  }

  public CsharpNamespaceDeclBuilder Record(string name, Action<CsharpRecordDeclBuilder> configuration = null)
  {
    var classBuilder = new CsharpRecordDeclBuilder(name);
    configuration?.Invoke(classBuilder);

    Node.Classes.Add(classBuilder.Build());

    return this;
  }

  public CsharpNamespaceDeclBuilder Interface(string name, Action<CsharpInterfaceDeclBuilder> configuration = null)
  {
    var interfaceBuilder = new CsharpInterfaceDeclBuilder(name);
    configuration?.Invoke(interfaceBuilder);

    Node.Classes.Add(interfaceBuilder.Build());

    return this;
  }

  public CsharpNamespaceDeclBuilder Comment(string text)
  {
    Node.Comment = new CsharpComment { Text = text };

    return this;
  }

  public CsharpNamespaceDeclBuilder XmlComment(Action<CsharpXmlCommentBuilder> configuration = null)
  {
    var commentBuilder = new CsharpXmlCommentBuilder();
    configuration?.Invoke(commentBuilder);
    Node.Comment = commentBuilder.Build();

    return this;
  }
}
