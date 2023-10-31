using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpRecordDeclBuilder : CsharpTypeDeclBuilder<CsharpRecordDeclBuilder, CsharpRecordDecl>
{
  public CsharpRecordDeclBuilder(string name)
    : base(name)
  {
  }

  public CsharpRecordDeclBuilder Parameters(Action<CsharpRecordParametersListBuilder> configuration = null)
  {
    var paramsBuilder = new CsharpRecordParametersListBuilder();
    configuration?.Invoke(paramsBuilder);

    foreach (var parameter in paramsBuilder.Build())
    {
      Node.Parameters.Add(parameter);
    }

    return this;
  }
}
