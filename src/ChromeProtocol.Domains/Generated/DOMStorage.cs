// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  /// <summary>Query and modify DOM storage.</summary>
  public static partial class DOMStorage
  {
    [Newtonsoft.Json.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record SerializedStorageKeyType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>DOM Storage identifier.</summary>
    /// <param name="IsLocalStorage">Whether the storage is local storage (not session storage).</param>
    /// <param name="SecurityOrigin">Security origin for the storage.</param>
    /// <param name="StorageKey">Represents a key by which DOM Storage keys its CachedStorageAreas</param>
    public record StorageIdType(
      [property: Newtonsoft.Json.JsonProperty("isLocalStorage")]
      bool IsLocalStorage,
      [property: Newtonsoft.Json.JsonProperty("securityOrigin")]
      string? SecurityOrigin = default,
      [property: Newtonsoft.Json.JsonProperty("storageKey")]
      ChromeProtocol.Domains.DOMStorage.SerializedStorageKeyType? StorageKey = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>DOM Storage item.</summary>
    [Newtonsoft.Json.JsonConverter(typeof(ChromeProtocol.Core.ArrayTypeConverter))]
    public record ItemType(
      System.Collections.Generic.IReadOnlyCollection<Newtonsoft.Json.Linq.JToken> Items
    ) : ChromeProtocol.Core.IArrayType
    {
    }
    [ChromeProtocol.Core.MethodName("DOMStorage.domStorageItemAdded")]
    public record DomStorageItemAdded(
      [property: Newtonsoft.Json.JsonProperty("storageId")]
      ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId,
      [property: Newtonsoft.Json.JsonProperty("key")]
      string Key,
      [property: Newtonsoft.Json.JsonProperty("newValue")]
      string NewValue
    ) : ChromeProtocol.Core.IEvent
    {
    }
    [ChromeProtocol.Core.MethodName("DOMStorage.domStorageItemRemoved")]
    public record DomStorageItemRemoved(
      [property: Newtonsoft.Json.JsonProperty("storageId")]
      ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId,
      [property: Newtonsoft.Json.JsonProperty("key")]
      string Key
    ) : ChromeProtocol.Core.IEvent
    {
    }
    [ChromeProtocol.Core.MethodName("DOMStorage.domStorageItemUpdated")]
    public record DomStorageItemUpdated(
      [property: Newtonsoft.Json.JsonProperty("storageId")]
      ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId,
      [property: Newtonsoft.Json.JsonProperty("key")]
      string Key,
      [property: Newtonsoft.Json.JsonProperty("oldValue")]
      string OldValue,
      [property: Newtonsoft.Json.JsonProperty("newValue")]
      string NewValue
    ) : ChromeProtocol.Core.IEvent
    {
    }
    [ChromeProtocol.Core.MethodName("DOMStorage.domStorageItemsCleared")]
    public record DomStorageItemsCleared(
      [property: Newtonsoft.Json.JsonProperty("storageId")]
      ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId
    ) : ChromeProtocol.Core.IEvent
    {
    }
    public static ChromeProtocol.Domains.DOMStorage.ClearRequest Clear(ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId)    
    {
      return new ChromeProtocol.Domains.DOMStorage.ClearRequest(StorageId);
    }
    [ChromeProtocol.Core.MethodName("DOMStorage.clear")]
    public record ClearRequest(
      [property: Newtonsoft.Json.JsonProperty("storageId")]
      ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId
    ) : ChromeProtocol.Core.ICommand<ClearRequestResult>
    {
    }
    public record ClearRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Disables storage tracking, prevents storage events from being sent to the client.</summary>
    public static ChromeProtocol.Domains.DOMStorage.DisableRequest Disable()    
    {
      return new ChromeProtocol.Domains.DOMStorage.DisableRequest();
    }
    /// <summary>Disables storage tracking, prevents storage events from being sent to the client.</summary>
    [ChromeProtocol.Core.MethodName("DOMStorage.disable")]
    public record DisableRequest() : ChromeProtocol.Core.ICommand<DisableRequestResult>
    {
    }
    public record DisableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Enables storage tracking, storage events will now be delivered to the client.</summary>
    public static ChromeProtocol.Domains.DOMStorage.EnableRequest Enable()    
    {
      return new ChromeProtocol.Domains.DOMStorage.EnableRequest();
    }
    /// <summary>Enables storage tracking, storage events will now be delivered to the client.</summary>
    [ChromeProtocol.Core.MethodName("DOMStorage.enable")]
    public record EnableRequest() : ChromeProtocol.Core.ICommand<EnableRequestResult>
    {
    }
    public record EnableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.DOMStorage.GetDOMStorageItemsRequest GetDOMStorageItems(ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId)    
    {
      return new ChromeProtocol.Domains.DOMStorage.GetDOMStorageItemsRequest(StorageId);
    }
    [ChromeProtocol.Core.MethodName("DOMStorage.getDOMStorageItems")]
    public record GetDOMStorageItemsRequest(
      [property: Newtonsoft.Json.JsonProperty("storageId")]
      ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId
    ) : ChromeProtocol.Core.ICommand<GetDOMStorageItemsRequestResult>
    {
    }
    public record GetDOMStorageItemsRequestResult(
      [property: Newtonsoft.Json.JsonProperty("entries")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.DOMStorage.ItemType> Entries
    ) : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.DOMStorage.RemoveDOMStorageItemRequest RemoveDOMStorageItem(ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId, string Key)    
    {
      return new ChromeProtocol.Domains.DOMStorage.RemoveDOMStorageItemRequest(StorageId, Key);
    }
    [ChromeProtocol.Core.MethodName("DOMStorage.removeDOMStorageItem")]
    public record RemoveDOMStorageItemRequest(
      [property: Newtonsoft.Json.JsonProperty("storageId")]
      ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId,
      [property: Newtonsoft.Json.JsonProperty("key")]
      string Key
    ) : ChromeProtocol.Core.ICommand<RemoveDOMStorageItemRequestResult>
    {
    }
    public record RemoveDOMStorageItemRequestResult() : ChromeProtocol.Core.IType
    {
    }
    public static ChromeProtocol.Domains.DOMStorage.SetDOMStorageItemRequest SetDOMStorageItem(ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId, string Key, string Value)    
    {
      return new ChromeProtocol.Domains.DOMStorage.SetDOMStorageItemRequest(StorageId, Key, Value);
    }
    [ChromeProtocol.Core.MethodName("DOMStorage.setDOMStorageItem")]
    public record SetDOMStorageItemRequest(
      [property: Newtonsoft.Json.JsonProperty("storageId")]
      ChromeProtocol.Domains.DOMStorage.StorageIdType StorageId,
      [property: Newtonsoft.Json.JsonProperty("key")]
      string Key,
      [property: Newtonsoft.Json.JsonProperty("value")]
      string Value
    ) : ChromeProtocol.Core.ICommand<SetDOMStorageItemRequestResult>
    {
    }
    public record SetDOMStorageItemRequestResult() : ChromeProtocol.Core.IType
    {
    }
  }
}
