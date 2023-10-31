using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpParametersListBuilder : CsharpCodeBuilder<List<CsharpMethodParameter>>
{
  public CsharpParametersListBuilder Parameter(string name, CsharpTypeInfo typeInfo, Action<CsharpMethodParameterBuilder>? configuration = null)
  {
    var parameterBuilder = new CsharpMethodParameterBuilder(name, typeInfo);
    configuration?.Invoke(parameterBuilder);
    Node.Add(parameterBuilder.Build());

    return this;
  }
}
