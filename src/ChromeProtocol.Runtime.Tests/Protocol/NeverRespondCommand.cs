using System.Text.Json.Serialization;
using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Tests.Protocol;

[MethodName("NeverRespond")]
public class NeverRespondCommand : ICommand<NeverRespondResult>
{
  [JsonIgnore]
  public string MethodName => "NeverRespond";
}

public record NeverRespondResult : IType;
