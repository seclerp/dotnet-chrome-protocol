// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  /// <summary>
  /// This domain allows inspection of Web Audio API.<br/>
  /// https://webaudio.github.io/web-audio-api/<br/>
  /// </summary>
  public static partial class WebAudio
  {
    /// <summary>An unique ID for a graph object (AudioContext, AudioNode, AudioParam) in Web Audio API</summary>
    [Newtonsoft.Json.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record GraphObjectIdType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>Enum of BaseAudioContext types</summary>
    [Newtonsoft.Json.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record ContextTypeType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>Enum of AudioContextState from the spec</summary>
    [Newtonsoft.Json.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record ContextStateType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>Enum of AudioNode types</summary>
    [Newtonsoft.Json.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record NodeTypeType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>Enum of AudioNode::ChannelCountMode from the spec</summary>
    [Newtonsoft.Json.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record ChannelCountModeType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>Enum of AudioNode::ChannelInterpretation from the spec</summary>
    [Newtonsoft.Json.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record ChannelInterpretationType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>Enum of AudioParam types</summary>
    [Newtonsoft.Json.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record ParamTypeType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>Enum of AudioParam::AutomationRate from the spec</summary>
    [Newtonsoft.Json.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record AutomationRateType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>Fields in AudioContext that change in real-time.</summary>
    /// <param name="CurrentTime">The current context time in second in BaseAudioContext.</param>
    /// <param name="RenderCapacity">
    /// The time spent on rendering graph divided by render quantum duration,<br/>
    /// and multiplied by 100. 100 means the audio renderer reached the full<br/>
    /// capacity and glitch may occur.<br/>
    /// </param>
    /// <param name="CallbackIntervalMean">A running mean of callback interval.</param>
    /// <param name="CallbackIntervalVariance">A running variance of callback interval.</param>
    public record ContextRealtimeDataType(
      [property: Newtonsoft.Json.JsonProperty("currentTime")]
      double CurrentTime,
      [property: Newtonsoft.Json.JsonProperty("renderCapacity")]
      double RenderCapacity,
      [property: Newtonsoft.Json.JsonProperty("callbackIntervalMean")]
      double CallbackIntervalMean,
      [property: Newtonsoft.Json.JsonProperty("callbackIntervalVariance")]
      double CallbackIntervalVariance
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Protocol object for BaseAudioContext</summary>
    /// <param name="CallbackBufferSize">Platform-dependent callback buffer size.</param>
    /// <param name="MaxOutputChannelCount">Number of output channels supported by audio hardware in use.</param>
    /// <param name="SampleRate">Context sample rate.</param>
    public record BaseAudioContextType(
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId,
      [property: Newtonsoft.Json.JsonProperty("contextType")]
      ChromeProtocol.Domains.WebAudio.ContextTypeType ContextType,
      [property: Newtonsoft.Json.JsonProperty("contextState")]
      ChromeProtocol.Domains.WebAudio.ContextStateType ContextState,
      [property: Newtonsoft.Json.JsonProperty("callbackBufferSize")]
      double CallbackBufferSize,
      [property: Newtonsoft.Json.JsonProperty("maxOutputChannelCount")]
      double MaxOutputChannelCount,
      [property: Newtonsoft.Json.JsonProperty("sampleRate")]
      double SampleRate,
      [property: Newtonsoft.Json.JsonProperty("realtimeData")]
      ChromeProtocol.Domains.WebAudio.ContextRealtimeDataType? RealtimeData = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Protocol object for AudioListener</summary>
    public record AudioListenerType(
      [property: Newtonsoft.Json.JsonProperty("listenerId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ListenerId,
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Protocol object for AudioNode</summary>
    public record AudioNodeType(
      [property: Newtonsoft.Json.JsonProperty("nodeId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType NodeId,
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId,
      [property: Newtonsoft.Json.JsonProperty("nodeType")]
      ChromeProtocol.Domains.WebAudio.NodeTypeType NodeType,
      [property: Newtonsoft.Json.JsonProperty("numberOfInputs")]
      double NumberOfInputs,
      [property: Newtonsoft.Json.JsonProperty("numberOfOutputs")]
      double NumberOfOutputs,
      [property: Newtonsoft.Json.JsonProperty("channelCount")]
      double ChannelCount,
      [property: Newtonsoft.Json.JsonProperty("channelCountMode")]
      ChromeProtocol.Domains.WebAudio.ChannelCountModeType ChannelCountMode,
      [property: Newtonsoft.Json.JsonProperty("channelInterpretation")]
      ChromeProtocol.Domains.WebAudio.ChannelInterpretationType ChannelInterpretation
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Protocol object for AudioParam</summary>
    public record AudioParamType(
      [property: Newtonsoft.Json.JsonProperty("paramId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ParamId,
      [property: Newtonsoft.Json.JsonProperty("nodeId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType NodeId,
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId,
      [property: Newtonsoft.Json.JsonProperty("paramType")]
      ChromeProtocol.Domains.WebAudio.ParamTypeType ParamType,
      [property: Newtonsoft.Json.JsonProperty("rate")]
      ChromeProtocol.Domains.WebAudio.AutomationRateType Rate,
      [property: Newtonsoft.Json.JsonProperty("defaultValue")]
      double DefaultValue,
      [property: Newtonsoft.Json.JsonProperty("minValue")]
      double MinValue,
      [property: Newtonsoft.Json.JsonProperty("maxValue")]
      double MaxValue
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Notifies that a new BaseAudioContext has been created.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.contextCreated")]
    public record ContextCreated(
      [property: Newtonsoft.Json.JsonProperty("context")]
      ChromeProtocol.Domains.WebAudio.BaseAudioContextType Context
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that an existing BaseAudioContext will be destroyed.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.contextWillBeDestroyed")]
    public record ContextWillBeDestroyed(
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that existing BaseAudioContext has changed some properties (id stays the same)..</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.contextChanged")]
    public record ContextChanged(
      [property: Newtonsoft.Json.JsonProperty("context")]
      ChromeProtocol.Domains.WebAudio.BaseAudioContextType Context
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that the construction of an AudioListener has finished.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.audioListenerCreated")]
    public record AudioListenerCreated(
      [property: Newtonsoft.Json.JsonProperty("listener")]
      ChromeProtocol.Domains.WebAudio.AudioListenerType Listener
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that a new AudioListener has been created.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.audioListenerWillBeDestroyed")]
    public record AudioListenerWillBeDestroyed(
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId,
      [property: Newtonsoft.Json.JsonProperty("listenerId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ListenerId
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that a new AudioNode has been created.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.audioNodeCreated")]
    public record AudioNodeCreated(
      [property: Newtonsoft.Json.JsonProperty("node")]
      ChromeProtocol.Domains.WebAudio.AudioNodeType Node
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that an existing AudioNode has been destroyed.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.audioNodeWillBeDestroyed")]
    public record AudioNodeWillBeDestroyed(
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId,
      [property: Newtonsoft.Json.JsonProperty("nodeId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType NodeId
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that a new AudioParam has been created.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.audioParamCreated")]
    public record AudioParamCreated(
      [property: Newtonsoft.Json.JsonProperty("param")]
      ChromeProtocol.Domains.WebAudio.AudioParamType Param
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that an existing AudioParam has been destroyed.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.audioParamWillBeDestroyed")]
    public record AudioParamWillBeDestroyed(
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId,
      [property: Newtonsoft.Json.JsonProperty("nodeId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType NodeId,
      [property: Newtonsoft.Json.JsonProperty("paramId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ParamId
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that two AudioNodes are connected.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.nodesConnected")]
    public record NodesConnected(
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId,
      [property: Newtonsoft.Json.JsonProperty("sourceId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType SourceId,
      [property: Newtonsoft.Json.JsonProperty("destinationId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType DestinationId,
      [property: Newtonsoft.Json.JsonProperty("sourceOutputIndex")]
      double? SourceOutputIndex = default,
      [property: Newtonsoft.Json.JsonProperty("destinationInputIndex")]
      double? DestinationInputIndex = default
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that AudioNodes are disconnected. The destination can be null, and it means all the outgoing connections from the source are disconnected.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.nodesDisconnected")]
    public record NodesDisconnected(
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId,
      [property: Newtonsoft.Json.JsonProperty("sourceId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType SourceId,
      [property: Newtonsoft.Json.JsonProperty("destinationId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType DestinationId,
      [property: Newtonsoft.Json.JsonProperty("sourceOutputIndex")]
      double? SourceOutputIndex = default,
      [property: Newtonsoft.Json.JsonProperty("destinationInputIndex")]
      double? DestinationInputIndex = default
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that an AudioNode is connected to an AudioParam.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.nodeParamConnected")]
    public record NodeParamConnected(
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId,
      [property: Newtonsoft.Json.JsonProperty("sourceId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType SourceId,
      [property: Newtonsoft.Json.JsonProperty("destinationId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType DestinationId,
      [property: Newtonsoft.Json.JsonProperty("sourceOutputIndex")]
      double? SourceOutputIndex = default
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Notifies that an AudioNode is disconnected to an AudioParam.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.nodeParamDisconnected")]
    public record NodeParamDisconnected(
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId,
      [property: Newtonsoft.Json.JsonProperty("sourceId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType SourceId,
      [property: Newtonsoft.Json.JsonProperty("destinationId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType DestinationId,
      [property: Newtonsoft.Json.JsonProperty("sourceOutputIndex")]
      double? SourceOutputIndex = default
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Enables the WebAudio domain and starts sending context lifetime events.</summary>
    public static ChromeProtocol.Domains.WebAudio.EnableRequest Enable()    
    {
      return new ChromeProtocol.Domains.WebAudio.EnableRequest();
    }
    /// <summary>Enables the WebAudio domain and starts sending context lifetime events.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.enable")]
    public record EnableRequest() : ChromeProtocol.Core.ICommand<EnableRequestResult>
    {
    }
    public record EnableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Disables the WebAudio domain.</summary>
    public static ChromeProtocol.Domains.WebAudio.DisableRequest Disable()    
    {
      return new ChromeProtocol.Domains.WebAudio.DisableRequest();
    }
    /// <summary>Disables the WebAudio domain.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.disable")]
    public record DisableRequest() : ChromeProtocol.Core.ICommand<DisableRequestResult>
    {
    }
    public record DisableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Fetch the realtime data from the registered contexts.</summary>
    public static ChromeProtocol.Domains.WebAudio.GetRealtimeDataRequest GetRealtimeData(ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId)    
    {
      return new ChromeProtocol.Domains.WebAudio.GetRealtimeDataRequest(ContextId);
    }
    /// <summary>Fetch the realtime data from the registered contexts.</summary>
    [ChromeProtocol.Core.MethodName("WebAudio.getRealtimeData")]
    public record GetRealtimeDataRequest(
      [property: Newtonsoft.Json.JsonProperty("contextId")]
      ChromeProtocol.Domains.WebAudio.GraphObjectIdType ContextId
    ) : ChromeProtocol.Core.ICommand<GetRealtimeDataRequestResult>
    {
    }
    public record GetRealtimeDataRequestResult(
      [property: Newtonsoft.Json.JsonProperty("realtimeData")]
      ChromeProtocol.Domains.WebAudio.ContextRealtimeDataType RealtimeData
    ) : ChromeProtocol.Core.IType
    {
    }
  }
}
