using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpRecordParametersListBuilder : CsharpCodeBuilder<List<CsharpRecordParameter>>
{
  public CsharpRecordParametersListBuilder Parameter(string name, CsharpTypeInfo typeInfo, Action<CsharpRecordParameterBuilder>? configuration = null)
  {
    var parameterBuilder = new CsharpRecordParameterBuilder(name, typeInfo);
    configuration?.Invoke(parameterBuilder);
    Node.Add(parameterBuilder.Build());

    return this;
  }
}
