using ChromeProtocol.Core;
using Newtonsoft.Json;

namespace ChromeProtocol.Runtime.Tests.Protocol;

[MethodName("Echo")]
public record EchoCommand(string Message) : ICommand<EchoResult>
{
  [JsonIgnore]
  public string MethodName => "Echo";
}

public record EchoResult(string Message) : IType;
