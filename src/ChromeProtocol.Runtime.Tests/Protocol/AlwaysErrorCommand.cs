using ChromeProtocol.Core;
using Newtonsoft.Json;

namespace ChromeProtocol.Runtime.Tests.Protocol;

[MethodName("AlwaysError")]
public class AlwaysErrorCommand : ICommand<AlwaysErrorResult>
{
  [JsonIgnore]
  public string MethodName => "AlwaysError";
}

public record AlwaysErrorResult : IType;
