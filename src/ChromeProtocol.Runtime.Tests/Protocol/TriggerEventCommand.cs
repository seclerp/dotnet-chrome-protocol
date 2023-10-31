using System.Text.Json.Serialization;
using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Tests.Protocol;

[MethodName("TriggerEvent")]
public class TriggerEventCommand : ICommand<TriggerEventResult>
{
  [JsonIgnore]
  public string MethodName => "TriggerEvent";
}

public record TriggerEventResult(string Message) : IType;
