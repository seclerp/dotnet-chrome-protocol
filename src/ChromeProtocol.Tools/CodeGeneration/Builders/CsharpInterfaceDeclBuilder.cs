using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpInterfaceDeclBuilder : CsharpTypeDeclBuilder<CsharpInterfaceDeclBuilder, CsharpInterfaceDecl>
{
  public CsharpInterfaceDeclBuilder(string name)
    : base(name)
  {
  }
}
