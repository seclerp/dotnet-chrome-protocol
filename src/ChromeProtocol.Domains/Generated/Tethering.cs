// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  /// <summary>The Tethering domain defines methods and events for browser port binding.</summary>
  public static partial class Tethering
  {
    /// <summary>Informs that port was successfully bound and got a specified connection id.</summary>
    /// <param name="Port">Port number that was successfully bound.</param>
    /// <param name="ConnectionId">Connection id to be used.</param>
    [ChromeProtocol.Core.MethodName("Tethering.accepted")]
    public record Accepted(
      [property: Newtonsoft.Json.JsonProperty("port")]
      int Port,
      [property: Newtonsoft.Json.JsonProperty("connectionId")]
      string ConnectionId
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Request browser port binding.</summary>
    /// <param name="Port">Port number to bind.</param>
    public static ChromeProtocol.Domains.Tethering.BindRequest Bind(int Port)    
    {
      return new ChromeProtocol.Domains.Tethering.BindRequest(Port);
    }
    /// <summary>Request browser port binding.</summary>
    /// <param name="Port">Port number to bind.</param>
    [ChromeProtocol.Core.MethodName("Tethering.bind")]
    public record BindRequest(
      [property: Newtonsoft.Json.JsonProperty("port")]
      int Port
    ) : ChromeProtocol.Core.ICommand<BindRequestResult>
    {
    }
    public record BindRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Request browser port unbinding.</summary>
    /// <param name="Port">Port number to unbind.</param>
    public static ChromeProtocol.Domains.Tethering.UnbindRequest Unbind(int Port)    
    {
      return new ChromeProtocol.Domains.Tethering.UnbindRequest(Port);
    }
    /// <summary>Request browser port unbinding.</summary>
    /// <param name="Port">Port number to unbind.</param>
    [ChromeProtocol.Core.MethodName("Tethering.unbind")]
    public record UnbindRequest(
      [property: Newtonsoft.Json.JsonProperty("port")]
      int Port
    ) : ChromeProtocol.Core.ICommand<UnbindRequestResult>
    {
    }
    public record UnbindRequestResult() : ChromeProtocol.Core.IType
    {
    }
  }
}
