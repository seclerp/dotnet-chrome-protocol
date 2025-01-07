// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  public static partial class ServiceWorker
  {
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record RegistrationIDType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>ServiceWorker registration.</summary>
    public record ServiceWorkerRegistrationType(
      [property: System.Text.Json.Serialization.JsonPropertyName("registrationId")]
      ChromeProtocol.Domains.ServiceWorker.RegistrationIDType RegistrationId,
      [property: System.Text.Json.Serialization.JsonPropertyName("scopeURL")]
      string ScopeURL,
      [property: System.Text.Json.Serialization.JsonPropertyName("isDeleted")]
      bool IsDeleted
    ) : ChromeProtocol.Core.IType
    {
    }
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record ServiceWorkerVersionRunningStatusType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record ServiceWorkerVersionStatusType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>ServiceWorker version.</summary>
    /// <param name="ScriptLastModified">The Last-Modified header value of the main script.</param>
    /// <param name="ScriptResponseTime">
    /// The time at which the response headers of the main script were received from the server.<br/>
    /// For cached script it is the last time the cache entry was validated.<br/>
    /// </param>
    public record ServiceWorkerVersionType(
      [property: System.Text.Json.Serialization.JsonPropertyName("versionId")]
      string VersionId,
      [property: System.Text.Json.Serialization.JsonPropertyName("registrationId")]
      ChromeProtocol.Domains.ServiceWorker.RegistrationIDType RegistrationId,
      [property: System.Text.Json.Serialization.JsonPropertyName("scriptURL")]
      string ScriptURL,
      [property: System.Text.Json.Serialization.JsonPropertyName("runningStatus")]
      ChromeProtocol.Domains.ServiceWorker.ServiceWorkerVersionRunningStatusType RunningStatus,
      [property: System.Text.Json.Serialization.JsonPropertyName("status")]
      ChromeProtocol.Domains.ServiceWorker.ServiceWorkerVersionStatusType Status,
      [property: System.Text.Json.Serialization.JsonPropertyName("scriptLastModified")]
      double? ScriptLastModified = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("scriptResponseTime")]
      double? ScriptResponseTime = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("controlledClients")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Target.TargetIDType>? ControlledClients = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("targetId")]
      ChromeProtocol.Domains.Target.TargetIDType? TargetId = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("routerRules")]
      string? RouterRules = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>ServiceWorker error message.</summary>
    public record ServiceWorkerErrorMessageType(
      [property: System.Text.Json.Serialization.JsonPropertyName("errorMessage")]
      string ErrorMessage,
      [property: System.Text.Json.Serialization.JsonPropertyName("registrationId")]
      ChromeProtocol.Domains.ServiceWorker.RegistrationIDType RegistrationId,
      [property: System.Text.Json.Serialization.JsonPropertyName("versionId")]
      string VersionId,
      [property: System.Text.Json.Serialization.JsonPropertyName("sourceURL")]
      string SourceURL,
      [property: System.Text.Json.Serialization.JsonPropertyName("lineNumber")]
      int LineNumber,
      [property: System.Text.Json.Serialization.JsonPropertyName("columnNumber")]
      int ColumnNumber
    ) : ChromeProtocol.Core.IType
    {
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.workerErrorReported")]
    public record WorkerErrorReported(
      [property: System.Text.Json.Serialization.JsonPropertyName("errorMessage")]
      ChromeProtocol.Domains.ServiceWorker.ServiceWorkerErrorMessageType ErrorMessage
    ) : ChromeProtocol.Core.IEvent
    {
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.workerRegistrationUpdated")]
    public record WorkerRegistrationUpdated(
      [property: System.Text.Json.Serialization.JsonPropertyName("registrations")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.ServiceWorker.ServiceWorkerRegistrationType> Registrations
    ) : ChromeProtocol.Core.IEvent
    {
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.workerVersionUpdated")]
    public record WorkerVersionUpdated(
      [property: System.Text.Json.Serialization.JsonPropertyName("versions")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.ServiceWorker.ServiceWorkerVersionType> Versions
    ) : ChromeProtocol.Core.IEvent
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.DeliverPushMessageRequest DeliverPushMessage(string Origin, ChromeProtocol.Domains.ServiceWorker.RegistrationIDType RegistrationId, string Data)    
    {
      return new ChromeProtocol.Domains.ServiceWorker.DeliverPushMessageRequest(Origin, RegistrationId, Data);
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.deliverPushMessage")]
    public record DeliverPushMessageRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("origin")]
      string Origin,
      [property: System.Text.Json.Serialization.JsonPropertyName("registrationId")]
      ChromeProtocol.Domains.ServiceWorker.RegistrationIDType RegistrationId,
      [property: System.Text.Json.Serialization.JsonPropertyName("data")]
      string Data
    ) : ChromeProtocol.Core.ICommand<DeliverPushMessageRequestResult>
    {
    }
    public record DeliverPushMessageRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.DisableRequest Disable()    
    {
      return new ChromeProtocol.Domains.ServiceWorker.DisableRequest();
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.disable")]
    public record DisableRequest() : ChromeProtocol.Core.ICommand<DisableRequestResult>
    {
    }
    public record DisableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.DispatchSyncEventRequest DispatchSyncEvent(string Origin, ChromeProtocol.Domains.ServiceWorker.RegistrationIDType RegistrationId, string Tag, bool LastChance)    
    {
      return new ChromeProtocol.Domains.ServiceWorker.DispatchSyncEventRequest(Origin, RegistrationId, Tag, LastChance);
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.dispatchSyncEvent")]
    public record DispatchSyncEventRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("origin")]
      string Origin,
      [property: System.Text.Json.Serialization.JsonPropertyName("registrationId")]
      ChromeProtocol.Domains.ServiceWorker.RegistrationIDType RegistrationId,
      [property: System.Text.Json.Serialization.JsonPropertyName("tag")]
      string Tag,
      [property: System.Text.Json.Serialization.JsonPropertyName("lastChance")]
      bool LastChance
    ) : ChromeProtocol.Core.ICommand<DispatchSyncEventRequestResult>
    {
    }
    public record DispatchSyncEventRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.DispatchPeriodicSyncEventRequest DispatchPeriodicSyncEvent(string Origin, ChromeProtocol.Domains.ServiceWorker.RegistrationIDType RegistrationId, string Tag)    
    {
      return new ChromeProtocol.Domains.ServiceWorker.DispatchPeriodicSyncEventRequest(Origin, RegistrationId, Tag);
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.dispatchPeriodicSyncEvent")]
    public record DispatchPeriodicSyncEventRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("origin")]
      string Origin,
      [property: System.Text.Json.Serialization.JsonPropertyName("registrationId")]
      ChromeProtocol.Domains.ServiceWorker.RegistrationIDType RegistrationId,
      [property: System.Text.Json.Serialization.JsonPropertyName("tag")]
      string Tag
    ) : ChromeProtocol.Core.ICommand<DispatchPeriodicSyncEventRequestResult>
    {
    }
    public record DispatchPeriodicSyncEventRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.EnableRequest Enable()    
    {
      return new ChromeProtocol.Domains.ServiceWorker.EnableRequest();
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.enable")]
    public record EnableRequest() : ChromeProtocol.Core.ICommand<EnableRequestResult>
    {
    }
    public record EnableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.InspectWorkerRequest InspectWorker(string VersionId)    
    {
      return new ChromeProtocol.Domains.ServiceWorker.InspectWorkerRequest(VersionId);
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.inspectWorker")]
    public record InspectWorkerRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("versionId")]
      string VersionId
    ) : ChromeProtocol.Core.ICommand<InspectWorkerRequestResult>
    {
    }
    public record InspectWorkerRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.SetForceUpdateOnPageLoadRequest SetForceUpdateOnPageLoad(bool ForceUpdateOnPageLoad)    
    {
      return new ChromeProtocol.Domains.ServiceWorker.SetForceUpdateOnPageLoadRequest(ForceUpdateOnPageLoad);
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.setForceUpdateOnPageLoad")]
    public record SetForceUpdateOnPageLoadRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("forceUpdateOnPageLoad")]
      bool ForceUpdateOnPageLoad
    ) : ChromeProtocol.Core.ICommand<SetForceUpdateOnPageLoadRequestResult>
    {
    }
    public record SetForceUpdateOnPageLoadRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.SkipWaitingRequest SkipWaiting(string ScopeURL)    
    {
      return new ChromeProtocol.Domains.ServiceWorker.SkipWaitingRequest(ScopeURL);
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.skipWaiting")]
    public record SkipWaitingRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("scopeURL")]
      string ScopeURL
    ) : ChromeProtocol.Core.ICommand<SkipWaitingRequestResult>
    {
    }
    public record SkipWaitingRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.StartWorkerRequest StartWorker(string ScopeURL)    
    {
      return new ChromeProtocol.Domains.ServiceWorker.StartWorkerRequest(ScopeURL);
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.startWorker")]
    public record StartWorkerRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("scopeURL")]
      string ScopeURL
    ) : ChromeProtocol.Core.ICommand<StartWorkerRequestResult>
    {
    }
    public record StartWorkerRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.StopAllWorkersRequest StopAllWorkers()    
    {
      return new ChromeProtocol.Domains.ServiceWorker.StopAllWorkersRequest();
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.stopAllWorkers")]
    public record StopAllWorkersRequest() : ChromeProtocol.Core.ICommand<StopAllWorkersRequestResult>
    {
    }
    public record StopAllWorkersRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.StopWorkerRequest StopWorker(string VersionId)    
    {
      return new ChromeProtocol.Domains.ServiceWorker.StopWorkerRequest(VersionId);
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.stopWorker")]
    public record StopWorkerRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("versionId")]
      string VersionId
    ) : ChromeProtocol.Core.ICommand<StopWorkerRequestResult>
    {
    }
    public record StopWorkerRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.UnregisterRequest Unregister(string ScopeURL)    
    {
      return new ChromeProtocol.Domains.ServiceWorker.UnregisterRequest(ScopeURL);
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.unregister")]
    public record UnregisterRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("scopeURL")]
      string ScopeURL
    ) : ChromeProtocol.Core.ICommand<UnregisterRequestResult>
    {
    }
    public record UnregisterRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.ServiceWorker.UpdateRegistrationRequest UpdateRegistration(string ScopeURL)    
    {
      return new ChromeProtocol.Domains.ServiceWorker.UpdateRegistrationRequest(ScopeURL);
    }
    [ChromeProtocol.Core.MethodName("ServiceWorker.updateRegistration")]
    public record UpdateRegistrationRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("scopeURL")]
      string ScopeURL
    ) : ChromeProtocol.Core.ICommand<UpdateRegistrationRequestResult>
    {
    }
    public record UpdateRegistrationRequestResult() : ChromeProtocol.Core.IType
    {
    }
  }
}
