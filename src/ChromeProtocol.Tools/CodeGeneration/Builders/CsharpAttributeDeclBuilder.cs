using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpAttributeDeclBuilder : CsharpCodeBuilder<CsharpAttributeDecl>
{
  public CsharpAttributeDeclBuilder(CsharpTypeInfo type)
  {
    Node.Type = type;
  }

  public CsharpAttributeDeclBuilder Target(string target)
  {
    Node.Target = target;

    return this;
  }

  public CsharpAttributeDeclBuilder Arguments(params string[] arguments)
  {
    Node.RawArguments = arguments;

    return this;
  }
}
