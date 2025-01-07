// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  /// <summary>
  /// DOM debugging allows setting breakpoints on particular DOM operations and events. JavaScript<br/>
  /// execution will stop on these operations as if there was a regular breakpoint set.<br/>
  /// </summary>
  public static partial class DOMDebugger
  {
    /// <summary>DOM breakpoint type.</summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record DOMBreakpointTypeType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>CSP Violation type.</summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record CSPViolationTypeType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>Object event listener.</summary>
    /// <param name="Type">`EventListener`&#39;s type.</param>
    /// <param name="UseCapture">`EventListener`&#39;s useCapture.</param>
    /// <param name="Passive">`EventListener`&#39;s passive flag.</param>
    /// <param name="Once">`EventListener`&#39;s once flag.</param>
    /// <param name="ScriptId">Script id of the handler code.</param>
    /// <param name="LineNumber">Line number in the script (0-based).</param>
    /// <param name="ColumnNumber">Column number in the script (0-based).</param>
    /// <param name="Handler">Event handler function value.</param>
    /// <param name="OriginalHandler">Event original handler function value.</param>
    /// <param name="BackendNodeId">Node the listener is added to (if any).</param>
    public record EventListenerType(
      [property: System.Text.Json.Serialization.JsonPropertyName("type")]
      string Type,
      [property: System.Text.Json.Serialization.JsonPropertyName("useCapture")]
      bool UseCapture,
      [property: System.Text.Json.Serialization.JsonPropertyName("passive")]
      bool Passive,
      [property: System.Text.Json.Serialization.JsonPropertyName("once")]
      bool Once,
      [property: System.Text.Json.Serialization.JsonPropertyName("scriptId")]
      ChromeProtocol.Domains.Runtime.ScriptIdType ScriptId,
      [property: System.Text.Json.Serialization.JsonPropertyName("lineNumber")]
      int LineNumber,
      [property: System.Text.Json.Serialization.JsonPropertyName("columnNumber")]
      int ColumnNumber,
      [property: System.Text.Json.Serialization.JsonPropertyName("handler")]
      ChromeProtocol.Domains.Runtime.RemoteObjectType? Handler = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("originalHandler")]
      ChromeProtocol.Domains.Runtime.RemoteObjectType? OriginalHandler = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("backendNodeId")]
      ChromeProtocol.Domains.DOM.BackendNodeIdType? BackendNodeId = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Returns event listeners of the given object.</summary>
    /// <param name="ObjectId">Identifier of the object to return listeners for.</param>
    /// <param name="Depth">
    /// The maximum depth at which Node children should be retrieved, defaults to 1. Use -1 for the<br/>
    /// entire subtree or provide an integer larger than 0.<br/>
    /// </param>
    /// <param name="Pierce">
    /// Whether or not iframes and shadow roots should be traversed when returning the subtree<br/>
    /// (default is false). Reports listeners for all contexts if pierce is enabled.<br/>
    /// </param>
    public static ChromeProtocol.Domains.DOMDebugger.GetEventListenersRequest GetEventListeners(ChromeProtocol.Domains.Runtime.RemoteObjectIdType ObjectId, int? Depth = default, bool? Pierce = default)    
    {
      return new ChromeProtocol.Domains.DOMDebugger.GetEventListenersRequest(ObjectId, Depth, Pierce);
    }
    /// <summary>Returns event listeners of the given object.</summary>
    /// <param name="ObjectId">Identifier of the object to return listeners for.</param>
    /// <param name="Depth">
    /// The maximum depth at which Node children should be retrieved, defaults to 1. Use -1 for the<br/>
    /// entire subtree or provide an integer larger than 0.<br/>
    /// </param>
    /// <param name="Pierce">
    /// Whether or not iframes and shadow roots should be traversed when returning the subtree<br/>
    /// (default is false). Reports listeners for all contexts if pierce is enabled.<br/>
    /// </param>
    [ChromeProtocol.Core.MethodName("DOMDebugger.getEventListeners")]
    public record GetEventListenersRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("objectId")]
      ChromeProtocol.Domains.Runtime.RemoteObjectIdType ObjectId,
      [property: System.Text.Json.Serialization.JsonPropertyName("depth")]
      int? Depth = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("pierce")]
      bool? Pierce = default
    ) : ChromeProtocol.Core.ICommand<GetEventListenersRequestResult>
    {
    }
    /// <param name="Listeners">Array of relevant listeners.</param>
    public record GetEventListenersRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("listeners")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.DOMDebugger.EventListenerType> Listeners
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Removes DOM breakpoint that was set using `setDOMBreakpoint`.</summary>
    /// <param name="NodeId">Identifier of the node to remove breakpoint from.</param>
    /// <param name="Type">Type of the breakpoint to remove.</param>
    public static ChromeProtocol.Domains.DOMDebugger.RemoveDOMBreakpointRequest RemoveDOMBreakpoint(ChromeProtocol.Domains.DOM.NodeIdType NodeId, ChromeProtocol.Domains.DOMDebugger.DOMBreakpointTypeType Type)    
    {
      return new ChromeProtocol.Domains.DOMDebugger.RemoveDOMBreakpointRequest(NodeId, Type);
    }
    /// <summary>Removes DOM breakpoint that was set using `setDOMBreakpoint`.</summary>
    /// <param name="NodeId">Identifier of the node to remove breakpoint from.</param>
    /// <param name="Type">Type of the breakpoint to remove.</param>
    [ChromeProtocol.Core.MethodName("DOMDebugger.removeDOMBreakpoint")]
    public record RemoveDOMBreakpointRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("nodeId")]
      ChromeProtocol.Domains.DOM.NodeIdType NodeId,
      [property: System.Text.Json.Serialization.JsonPropertyName("type")]
      ChromeProtocol.Domains.DOMDebugger.DOMBreakpointTypeType Type
    ) : ChromeProtocol.Core.ICommand<RemoveDOMBreakpointRequestResult>
    {
    }
    public record RemoveDOMBreakpointRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Removes breakpoint on particular DOM event.</summary>
    /// <param name="EventName">Event name.</param>
    /// <param name="TargetName">EventTarget interface name.</param>
    public static ChromeProtocol.Domains.DOMDebugger.RemoveEventListenerBreakpointRequest RemoveEventListenerBreakpoint(string EventName, string? TargetName = default)    
    {
      return new ChromeProtocol.Domains.DOMDebugger.RemoveEventListenerBreakpointRequest(EventName, TargetName);
    }
    /// <summary>Removes breakpoint on particular DOM event.</summary>
    /// <param name="EventName">Event name.</param>
    /// <param name="TargetName">EventTarget interface name.</param>
    [ChromeProtocol.Core.MethodName("DOMDebugger.removeEventListenerBreakpoint")]
    public record RemoveEventListenerBreakpointRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("eventName")]
      string EventName,
      [property: System.Text.Json.Serialization.JsonPropertyName("targetName")]
      string? TargetName = default
    ) : ChromeProtocol.Core.ICommand<RemoveEventListenerBreakpointRequestResult>
    {
    }
    public record RemoveEventListenerBreakpointRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Removes breakpoint on particular native event.</summary>
    /// <param name="EventName">Instrumentation name to stop on.</param>
    [System.Obsolete("This command marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.", false)]
    public static ChromeProtocol.Domains.DOMDebugger.RemoveInstrumentationBreakpointRequest RemoveInstrumentationBreakpoint(string EventName)    
    {
      return new ChromeProtocol.Domains.DOMDebugger.RemoveInstrumentationBreakpointRequest(EventName);
    }
    /// <summary>Removes breakpoint on particular native event.</summary>
    /// <param name="EventName">Instrumentation name to stop on.</param>
    [ChromeProtocol.Core.MethodName("DOMDebugger.removeInstrumentationBreakpoint")]
    [System.Obsolete("This command marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.", false)]
    public record RemoveInstrumentationBreakpointRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("eventName")]
      string EventName
    ) : ChromeProtocol.Core.ICommand<RemoveInstrumentationBreakpointRequestResult>
    {
    }
    [System.Obsolete("This command marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.", false)]
    public record RemoveInstrumentationBreakpointRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Removes breakpoint from XMLHttpRequest.</summary>
    /// <param name="Url">Resource URL substring.</param>
    public static ChromeProtocol.Domains.DOMDebugger.RemoveXHRBreakpointRequest RemoveXHRBreakpoint(string Url)    
    {
      return new ChromeProtocol.Domains.DOMDebugger.RemoveXHRBreakpointRequest(Url);
    }
    /// <summary>Removes breakpoint from XMLHttpRequest.</summary>
    /// <param name="Url">Resource URL substring.</param>
    [ChromeProtocol.Core.MethodName("DOMDebugger.removeXHRBreakpoint")]
    public record RemoveXHRBreakpointRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("url")]
      string Url
    ) : ChromeProtocol.Core.ICommand<RemoveXHRBreakpointRequestResult>
    {
    }
    public record RemoveXHRBreakpointRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Sets breakpoint on particular CSP violations.</summary>
    /// <param name="ViolationTypes">CSP Violations to stop upon.</param>
    public static ChromeProtocol.Domains.DOMDebugger.SetBreakOnCSPViolationRequest SetBreakOnCSPViolation(System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.DOMDebugger.CSPViolationTypeType> ViolationTypes)    
    {
      return new ChromeProtocol.Domains.DOMDebugger.SetBreakOnCSPViolationRequest(ViolationTypes);
    }
    /// <summary>Sets breakpoint on particular CSP violations.</summary>
    /// <param name="ViolationTypes">CSP Violations to stop upon.</param>
    [ChromeProtocol.Core.MethodName("DOMDebugger.setBreakOnCSPViolation")]
    public record SetBreakOnCSPViolationRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("violationTypes")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.DOMDebugger.CSPViolationTypeType> ViolationTypes
    ) : ChromeProtocol.Core.ICommand<SetBreakOnCSPViolationRequestResult>
    {
    }
    public record SetBreakOnCSPViolationRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Sets breakpoint on particular operation with DOM.</summary>
    /// <param name="NodeId">Identifier of the node to set breakpoint on.</param>
    /// <param name="Type">Type of the operation to stop upon.</param>
    public static ChromeProtocol.Domains.DOMDebugger.SetDOMBreakpointRequest SetDOMBreakpoint(ChromeProtocol.Domains.DOM.NodeIdType NodeId, ChromeProtocol.Domains.DOMDebugger.DOMBreakpointTypeType Type)    
    {
      return new ChromeProtocol.Domains.DOMDebugger.SetDOMBreakpointRequest(NodeId, Type);
    }
    /// <summary>Sets breakpoint on particular operation with DOM.</summary>
    /// <param name="NodeId">Identifier of the node to set breakpoint on.</param>
    /// <param name="Type">Type of the operation to stop upon.</param>
    [ChromeProtocol.Core.MethodName("DOMDebugger.setDOMBreakpoint")]
    public record SetDOMBreakpointRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("nodeId")]
      ChromeProtocol.Domains.DOM.NodeIdType NodeId,
      [property: System.Text.Json.Serialization.JsonPropertyName("type")]
      ChromeProtocol.Domains.DOMDebugger.DOMBreakpointTypeType Type
    ) : ChromeProtocol.Core.ICommand<SetDOMBreakpointRequestResult>
    {
    }
    public record SetDOMBreakpointRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Sets breakpoint on particular DOM event.</summary>
    /// <param name="EventName">DOM Event name to stop on (any DOM event will do).</param>
    /// <param name="TargetName">
    /// EventTarget interface name to stop on. If equal to `&quot;*&quot;` or not provided, will stop on any<br/>
    /// EventTarget.<br/>
    /// </param>
    public static ChromeProtocol.Domains.DOMDebugger.SetEventListenerBreakpointRequest SetEventListenerBreakpoint(string EventName, string? TargetName = default)    
    {
      return new ChromeProtocol.Domains.DOMDebugger.SetEventListenerBreakpointRequest(EventName, TargetName);
    }
    /// <summary>Sets breakpoint on particular DOM event.</summary>
    /// <param name="EventName">DOM Event name to stop on (any DOM event will do).</param>
    /// <param name="TargetName">
    /// EventTarget interface name to stop on. If equal to `&quot;*&quot;` or not provided, will stop on any<br/>
    /// EventTarget.<br/>
    /// </param>
    [ChromeProtocol.Core.MethodName("DOMDebugger.setEventListenerBreakpoint")]
    public record SetEventListenerBreakpointRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("eventName")]
      string EventName,
      [property: System.Text.Json.Serialization.JsonPropertyName("targetName")]
      string? TargetName = default
    ) : ChromeProtocol.Core.ICommand<SetEventListenerBreakpointRequestResult>
    {
    }
    public record SetEventListenerBreakpointRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Sets breakpoint on particular native event.</summary>
    /// <param name="EventName">Instrumentation name to stop on.</param>
    [System.Obsolete("This command marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.", false)]
    public static ChromeProtocol.Domains.DOMDebugger.SetInstrumentationBreakpointRequest SetInstrumentationBreakpoint(string EventName)    
    {
      return new ChromeProtocol.Domains.DOMDebugger.SetInstrumentationBreakpointRequest(EventName);
    }
    /// <summary>Sets breakpoint on particular native event.</summary>
    /// <param name="EventName">Instrumentation name to stop on.</param>
    [ChromeProtocol.Core.MethodName("DOMDebugger.setInstrumentationBreakpoint")]
    [System.Obsolete("This command marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.", false)]
    public record SetInstrumentationBreakpointRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("eventName")]
      string EventName
    ) : ChromeProtocol.Core.ICommand<SetInstrumentationBreakpointRequestResult>
    {
    }
    [System.Obsolete("This command marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.", false)]
    public record SetInstrumentationBreakpointRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Sets breakpoint on XMLHttpRequest.</summary>
    /// <param name="Url">Resource URL substring. All XHRs having this substring in the URL will get stopped upon.</param>
    public static ChromeProtocol.Domains.DOMDebugger.SetXHRBreakpointRequest SetXHRBreakpoint(string Url)    
    {
      return new ChromeProtocol.Domains.DOMDebugger.SetXHRBreakpointRequest(Url);
    }
    /// <summary>Sets breakpoint on XMLHttpRequest.</summary>
    /// <param name="Url">Resource URL substring. All XHRs having this substring in the URL will get stopped upon.</param>
    [ChromeProtocol.Core.MethodName("DOMDebugger.setXHRBreakpoint")]
    public record SetXHRBreakpointRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("url")]
      string Url
    ) : ChromeProtocol.Core.ICommand<SetXHRBreakpointRequestResult>
    {
    }
    public record SetXHRBreakpointRequestResult() : ChromeProtocol.Core.IType
    {
    }
  }
}
