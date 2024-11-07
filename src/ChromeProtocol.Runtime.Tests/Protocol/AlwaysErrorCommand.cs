using System.Text.Json.Serialization;
using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Tests.Protocol;

[MethodName("AlwaysError")]
public class AlwaysErrorCommand : ICommand<AlwaysErrorResult>
{
  [JsonIgnore]
  public string MethodName => "AlwaysError";
}

public record AlwaysErrorResult : IType;
