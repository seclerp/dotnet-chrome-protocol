// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  /// <summary>This domain allows detailed inspection of media elements</summary>
  public static partial class Media
  {
    /// <summary>Players will get an ID that is unique within the agent context.</summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record PlayerIdType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record TimestampType(
      double Value
    ) : ChromeProtocol.Core.PrimitiveType<double>(Value)
    {
    }
    /// <summary>
    /// Have one type per entry in MediaLogRecord::Type<br/>
    /// Corresponds to kMessage<br/>
    /// </summary>
    /// <param name="Level">
    /// Keep in sync with MediaLogMessageLevel<br/>
    /// We are currently keeping the message level &#39;error&#39; separate from the<br/>
    /// PlayerError type because right now they represent different things,<br/>
    /// this one being a DVLOG(ERROR) style log message that gets printed<br/>
    /// based on what log level is selected in the UI, and the other is a<br/>
    /// representation of a media::PipelineStatus object. Soon however we&#39;re<br/>
    /// going to be moving away from using PipelineStatus for errors and<br/>
    /// introducing a new error type which should hopefully let us integrate<br/>
    /// the error log level into the PlayerError type.<br/>
    /// </param>
    public record PlayerMessageType(
      [property: System.Text.Json.Serialization.JsonPropertyName("level")]
      string Level,
      [property: System.Text.Json.Serialization.JsonPropertyName("message")]
      string Message
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Corresponds to kMediaPropertyChange</summary>
    public record PlayerPropertyType(
      [property: System.Text.Json.Serialization.JsonPropertyName("name")]
      string Name,
      [property: System.Text.Json.Serialization.JsonPropertyName("value")]
      string Value
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Corresponds to kMediaEventTriggered</summary>
    public record PlayerEventType(
      [property: System.Text.Json.Serialization.JsonPropertyName("timestamp")]
      ChromeProtocol.Domains.Media.TimestampType Timestamp,
      [property: System.Text.Json.Serialization.JsonPropertyName("value")]
      string Value
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>
    /// Represents logged source line numbers reported in an error.<br/>
    /// NOTE: file and line are from chromium c++ implementation code, not js.<br/>
    /// </summary>
    public record PlayerErrorSourceLocationType(
      [property: System.Text.Json.Serialization.JsonPropertyName("file")]
      string File,
      [property: System.Text.Json.Serialization.JsonPropertyName("line")]
      int Line
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Corresponds to kMediaError</summary>
    /// <param name="Code">
    /// Code is the numeric enum entry for a specific set of error codes, such<br/>
    /// as PipelineStatusCodes in media/base/pipeline_status.h<br/>
    /// </param>
    /// <param name="Stack">A trace of where this error was caused / where it passed through.</param>
    /// <param name="Cause">
    /// Errors potentially have a root cause error, ie, a DecoderError might be<br/>
    /// caused by an WindowsError<br/>
    /// </param>
    /// <param name="Data">Extra data attached to an error, such as an HRESULT, Video Codec, etc.</param>
    public record PlayerErrorType(
      [property: System.Text.Json.Serialization.JsonPropertyName("errorType")]
      string ErrorType,
      [property: System.Text.Json.Serialization.JsonPropertyName("code")]
      int Code,
      [property: System.Text.Json.Serialization.JsonPropertyName("stack")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Media.PlayerErrorSourceLocationType> Stack,
      [property: System.Text.Json.Serialization.JsonPropertyName("cause")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Media.PlayerErrorType> Cause,
      [property: System.Text.Json.Serialization.JsonPropertyName("data")]
      System.Text.Json.Nodes.JsonObject Data
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>
    /// This can be called multiple times, and can be used to set / override /<br/>
    /// remove player properties. A null propValue indicates removal.<br/>
    /// </summary>
    [ChromeProtocol.Core.MethodName("Media.playerPropertiesChanged")]
    public record PlayerPropertiesChanged(
      [property: System.Text.Json.Serialization.JsonPropertyName("playerId")]
      ChromeProtocol.Domains.Media.PlayerIdType PlayerId,
      [property: System.Text.Json.Serialization.JsonPropertyName("properties")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Media.PlayerPropertyType> Properties
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>
    /// Send events as a list, allowing them to be batched on the browser for less<br/>
    /// congestion. If batched, events must ALWAYS be in chronological order.<br/>
    /// </summary>
    [ChromeProtocol.Core.MethodName("Media.playerEventsAdded")]
    public record PlayerEventsAdded(
      [property: System.Text.Json.Serialization.JsonPropertyName("playerId")]
      ChromeProtocol.Domains.Media.PlayerIdType PlayerId,
      [property: System.Text.Json.Serialization.JsonPropertyName("events")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Media.PlayerEventType> Events
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Send a list of any messages that need to be delivered.</summary>
    [ChromeProtocol.Core.MethodName("Media.playerMessagesLogged")]
    public record PlayerMessagesLogged(
      [property: System.Text.Json.Serialization.JsonPropertyName("playerId")]
      ChromeProtocol.Domains.Media.PlayerIdType PlayerId,
      [property: System.Text.Json.Serialization.JsonPropertyName("messages")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Media.PlayerMessageType> Messages
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Send a list of any errors that need to be delivered.</summary>
    [ChromeProtocol.Core.MethodName("Media.playerErrorsRaised")]
    public record PlayerErrorsRaised(
      [property: System.Text.Json.Serialization.JsonPropertyName("playerId")]
      ChromeProtocol.Domains.Media.PlayerIdType PlayerId,
      [property: System.Text.Json.Serialization.JsonPropertyName("errors")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Media.PlayerErrorType> Errors
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>
    /// Called whenever a player is created, or when a new agent joins and receives<br/>
    /// a list of active players. If an agent is restored, it will receive the full<br/>
    /// list of player ids and all events again.<br/>
    /// </summary>
    [ChromeProtocol.Core.MethodName("Media.playersCreated")]
    public record PlayersCreated(
      [property: System.Text.Json.Serialization.JsonPropertyName("players")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Media.PlayerIdType> Players
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Enables the Media domain</summary>
    public static ChromeProtocol.Domains.Media.EnableRequest Enable()    
    {
      return new ChromeProtocol.Domains.Media.EnableRequest();
    }
    /// <summary>Enables the Media domain</summary>
    [ChromeProtocol.Core.MethodName("Media.enable")]
    public record EnableRequest() : ChromeProtocol.Core.ICommand<EnableRequestResult>
    {
    }
    public record EnableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Disables the Media domain.</summary>
    public static ChromeProtocol.Domains.Media.DisableRequest Disable()    
    {
      return new ChromeProtocol.Domains.Media.DisableRequest();
    }
    /// <summary>Disables the Media domain.</summary>
    [ChromeProtocol.Core.MethodName("Media.disable")]
    public record DisableRequest() : ChromeProtocol.Core.ICommand<DisableRequestResult>
    {
    }
    public record DisableRequestResult() : ChromeProtocol.Core.IType
    {
    }
  }
}
