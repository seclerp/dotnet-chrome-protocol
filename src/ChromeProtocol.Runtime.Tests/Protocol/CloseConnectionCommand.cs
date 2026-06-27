using System.Text.Json.Serialization;
using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Tests.Protocol;

[MethodName("CloseConnection")]
public class CloseConnectionCommand : ICommand<CloseConnectionResult>
{
  [JsonIgnore]
  public string MethodName => "CloseConnection";
}

public record CloseConnectionResult : IType;
