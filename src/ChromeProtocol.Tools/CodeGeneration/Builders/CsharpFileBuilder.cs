using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpFileBuilder : CsharpCodeBuilder<CsharpFile>
{
  protected CsharpFileBuilder() { }

  public static CsharpFileBuilder Create()
  {
    return new CsharpFileBuilder();
  }

  public CsharpFileBuilder Using(string nameSpace)
  {
    Node.Usings.Add(nameSpace);

    return this;
  }

  public CsharpFileBuilder Namespace(string fullName, Action<CsharpNamespaceDeclBuilder>? configuration = null)
  {
    var classBuilder = new CsharpNamespaceDeclBuilder(fullName);
    configuration?.Invoke(classBuilder);

    Node.Namespaces.Add(classBuilder.Build());

    return this;
  }
}
