// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  /// <summary>
  /// EventBreakpoints permits setting JavaScript breakpoints on operations and events<br/>
  /// occurring in native code invoked from JavaScript. Once breakpoint is hit, it is<br/>
  /// reported through Debugger domain, similarly to regular breakpoints being hit.<br/>
  /// </summary>
  public static partial class EventBreakpoints
  {
    /// <summary>Sets breakpoint on particular native event.</summary>
    /// <param name="EventName">Instrumentation name to stop on.</param>
    public static ChromeProtocol.Domains.EventBreakpoints.SetInstrumentationBreakpointRequest SetInstrumentationBreakpoint(string EventName)    
    {
      return new ChromeProtocol.Domains.EventBreakpoints.SetInstrumentationBreakpointRequest(EventName);
    }
    /// <summary>Sets breakpoint on particular native event.</summary>
    /// <param name="EventName">Instrumentation name to stop on.</param>
    [ChromeProtocol.Core.MethodName("EventBreakpoints.setInstrumentationBreakpoint")]
    public record SetInstrumentationBreakpointRequest(
      [property: Newtonsoft.Json.JsonProperty("eventName")]
      string EventName
    ) : ChromeProtocol.Core.ICommand<SetInstrumentationBreakpointRequestResult>
    {
    }
    public record SetInstrumentationBreakpointRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Removes breakpoint on particular native event.</summary>
    /// <param name="EventName">Instrumentation name to stop on.</param>
    public static ChromeProtocol.Domains.EventBreakpoints.RemoveInstrumentationBreakpointRequest RemoveInstrumentationBreakpoint(string EventName)    
    {
      return new ChromeProtocol.Domains.EventBreakpoints.RemoveInstrumentationBreakpointRequest(EventName);
    }
    /// <summary>Removes breakpoint on particular native event.</summary>
    /// <param name="EventName">Instrumentation name to stop on.</param>
    [ChromeProtocol.Core.MethodName("EventBreakpoints.removeInstrumentationBreakpoint")]
    public record RemoveInstrumentationBreakpointRequest(
      [property: Newtonsoft.Json.JsonProperty("eventName")]
      string EventName
    ) : ChromeProtocol.Core.ICommand<RemoveInstrumentationBreakpointRequestResult>
    {
    }
    public record RemoveInstrumentationBreakpointRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Removes all breakpoints</summary>
    public static ChromeProtocol.Domains.EventBreakpoints.DisableRequest Disable()    
    {
      return new ChromeProtocol.Domains.EventBreakpoints.DisableRequest();
    }
    /// <summary>Removes all breakpoints</summary>
    [ChromeProtocol.Core.MethodName("EventBreakpoints.disable")]
    public record DisableRequest() : ChromeProtocol.Core.ICommand<DisableRequestResult>
    {
    }
    public record DisableRequestResult() : ChromeProtocol.Core.IType
    {
    }
  }
}
