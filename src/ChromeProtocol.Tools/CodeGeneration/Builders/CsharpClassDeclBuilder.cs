using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpClassDeclBuilder : CsharpTypeDeclBuilder<CsharpClassDeclBuilder, CsharpClassDecl>
{
  public CsharpClassDeclBuilder(string name)
    : base(name)
  {
  }

  public CsharpClassDeclBuilder Constructor(Action<CsharpMethodDeclBuilder>? configuration = null)
  {
    var methodBuilder = new CsharpMethodDeclBuilder(Node.Name);
    methodBuilder.ReturnType(null);
    configuration?.Invoke(methodBuilder);

    Node.Members.Add(methodBuilder.Build());

    return this;
  }

  public CsharpClassDeclBuilder Field(string name, CsharpTypeInfo typeInfo, Action<CsharpFieldDeclBuilder>? configuration = null)
  {
    var fieldBuilder = new CsharpFieldDeclBuilder(name, typeInfo);
    configuration?.Invoke(fieldBuilder);

    Node.Members.Add(fieldBuilder.Build());

    return this;
  }
}
