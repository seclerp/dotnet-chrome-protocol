// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  public static partial class CacheStorage
  {
    /// <summary>Unique identifier of the Cache object.</summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record CacheIdType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>type of HTTP response cached</summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record CachedResponseTypeType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>Data entry.</summary>
    /// <param name="RequestURL">Request URL.</param>
    /// <param name="RequestMethod">Request method.</param>
    /// <param name="RequestHeaders">Request headers</param>
    /// <param name="ResponseTime">Number of seconds since epoch.</param>
    /// <param name="ResponseStatus">HTTP response status code.</param>
    /// <param name="ResponseStatusText">HTTP response status text.</param>
    /// <param name="ResponseType">HTTP response type</param>
    /// <param name="ResponseHeaders">Response headers</param>
    public record DataEntryType(
      [property: System.Text.Json.Serialization.JsonPropertyName("requestURL")]
      string RequestURL,
      [property: System.Text.Json.Serialization.JsonPropertyName("requestMethod")]
      string RequestMethod,
      [property: System.Text.Json.Serialization.JsonPropertyName("requestHeaders")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.CacheStorage.HeaderType> RequestHeaders,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseTime")]
      double ResponseTime,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseStatus")]
      int ResponseStatus,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseStatusText")]
      string ResponseStatusText,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseType")]
      ChromeProtocol.Domains.CacheStorage.CachedResponseTypeType ResponseType,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseHeaders")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.CacheStorage.HeaderType> ResponseHeaders
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Cache identifier.</summary>
    /// <param name="CacheId">An opaque unique id of the cache.</param>
    /// <param name="SecurityOrigin">Security origin of the cache.</param>
    /// <param name="StorageKey">Storage key of the cache.</param>
    /// <param name="CacheName">The name of the cache.</param>
    /// <param name="StorageBucket">Storage bucket of the cache.</param>
    public record CacheType(
      [property: System.Text.Json.Serialization.JsonPropertyName("cacheId")]
      ChromeProtocol.Domains.CacheStorage.CacheIdType CacheId,
      [property: System.Text.Json.Serialization.JsonPropertyName("securityOrigin")]
      string SecurityOrigin,
      [property: System.Text.Json.Serialization.JsonPropertyName("storageKey")]
      string StorageKey,
      [property: System.Text.Json.Serialization.JsonPropertyName("cacheName")]
      string CacheName,
      [property: System.Text.Json.Serialization.JsonPropertyName("storageBucket")]
      ChromeProtocol.Domains.Storage.StorageBucketType? StorageBucket = default
    ) : ChromeProtocol.Core.IType
    {
    }
    public record HeaderType(
      [property: System.Text.Json.Serialization.JsonPropertyName("name")]
      string Name,
      [property: System.Text.Json.Serialization.JsonPropertyName("value")]
      string Value
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Cached response</summary>
    /// <param name="Body">Entry content, base64-encoded. (Encoded as a base64 string when passed over JSON)</param>
    public record CachedResponseType(
      [property: System.Text.Json.Serialization.JsonPropertyName("body")]
      string Body
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Deletes a cache.</summary>
    /// <param name="CacheId">Id of cache for deletion.</param>
    public static ChromeProtocol.Domains.CacheStorage.DeleteCacheRequest DeleteCache(ChromeProtocol.Domains.CacheStorage.CacheIdType CacheId)    
    {
      return new ChromeProtocol.Domains.CacheStorage.DeleteCacheRequest(CacheId);
    }
    /// <summary>Deletes a cache.</summary>
    /// <param name="CacheId">Id of cache for deletion.</param>
    [ChromeProtocol.Core.MethodName("CacheStorage.deleteCache")]
    public record DeleteCacheRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("cacheId")]
      ChromeProtocol.Domains.CacheStorage.CacheIdType CacheId
    ) : ChromeProtocol.Core.ICommand<DeleteCacheRequestResult>
    {
    }
    public record DeleteCacheRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Deletes a cache entry.</summary>
    /// <param name="CacheId">Id of cache where the entry will be deleted.</param>
    /// <param name="Request">URL spec of the request.</param>
    public static ChromeProtocol.Domains.CacheStorage.DeleteEntryRequest DeleteEntry(ChromeProtocol.Domains.CacheStorage.CacheIdType CacheId, string Request)    
    {
      return new ChromeProtocol.Domains.CacheStorage.DeleteEntryRequest(CacheId, Request);
    }
    /// <summary>Deletes a cache entry.</summary>
    /// <param name="CacheId">Id of cache where the entry will be deleted.</param>
    /// <param name="Request">URL spec of the request.</param>
    [ChromeProtocol.Core.MethodName("CacheStorage.deleteEntry")]
    public record DeleteEntryRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("cacheId")]
      ChromeProtocol.Domains.CacheStorage.CacheIdType CacheId,
      [property: System.Text.Json.Serialization.JsonPropertyName("request")]
      string Request
    ) : ChromeProtocol.Core.ICommand<DeleteEntryRequestResult>
    {
    }
    public record DeleteEntryRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Requests cache names.</summary>
    /// <param name="SecurityOrigin">
    /// At least and at most one of securityOrigin, storageKey, storageBucket must be specified.<br/>
    /// Security origin.<br/>
    /// </param>
    /// <param name="StorageKey">Storage key.</param>
    /// <param name="StorageBucket">Storage bucket. If not specified, it uses the default bucket.</param>
    public static ChromeProtocol.Domains.CacheStorage.RequestCacheNamesRequest RequestCacheNames(string? SecurityOrigin = default, string? StorageKey = default, ChromeProtocol.Domains.Storage.StorageBucketType? StorageBucket = default)    
    {
      return new ChromeProtocol.Domains.CacheStorage.RequestCacheNamesRequest(SecurityOrigin, StorageKey, StorageBucket);
    }
    /// <summary>Requests cache names.</summary>
    /// <param name="SecurityOrigin">
    /// At least and at most one of securityOrigin, storageKey, storageBucket must be specified.<br/>
    /// Security origin.<br/>
    /// </param>
    /// <param name="StorageKey">Storage key.</param>
    /// <param name="StorageBucket">Storage bucket. If not specified, it uses the default bucket.</param>
    [ChromeProtocol.Core.MethodName("CacheStorage.requestCacheNames")]
    public record RequestCacheNamesRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("securityOrigin")]
      string? SecurityOrigin = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("storageKey")]
      string? StorageKey = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("storageBucket")]
      ChromeProtocol.Domains.Storage.StorageBucketType? StorageBucket = default
    ) : ChromeProtocol.Core.ICommand<RequestCacheNamesRequestResult>
    {
    }
    /// <param name="Caches">Caches for the security origin.</param>
    public record RequestCacheNamesRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("caches")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.CacheStorage.CacheType> Caches
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Fetches cache entry.</summary>
    /// <param name="CacheId">Id of cache that contains the entry.</param>
    /// <param name="RequestURL">URL spec of the request.</param>
    /// <param name="RequestHeaders">headers of the request.</param>
    public static ChromeProtocol.Domains.CacheStorage.RequestCachedResponseRequest RequestCachedResponse(ChromeProtocol.Domains.CacheStorage.CacheIdType CacheId, string RequestURL, System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.CacheStorage.HeaderType> RequestHeaders)    
    {
      return new ChromeProtocol.Domains.CacheStorage.RequestCachedResponseRequest(CacheId, RequestURL, RequestHeaders);
    }
    /// <summary>Fetches cache entry.</summary>
    /// <param name="CacheId">Id of cache that contains the entry.</param>
    /// <param name="RequestURL">URL spec of the request.</param>
    /// <param name="RequestHeaders">headers of the request.</param>
    [ChromeProtocol.Core.MethodName("CacheStorage.requestCachedResponse")]
    public record RequestCachedResponseRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("cacheId")]
      ChromeProtocol.Domains.CacheStorage.CacheIdType CacheId,
      [property: System.Text.Json.Serialization.JsonPropertyName("requestURL")]
      string RequestURL,
      [property: System.Text.Json.Serialization.JsonPropertyName("requestHeaders")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.CacheStorage.HeaderType> RequestHeaders
    ) : ChromeProtocol.Core.ICommand<RequestCachedResponseRequestResult>
    {
    }
    /// <param name="Response">Response read from the cache.</param>
    public record RequestCachedResponseRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("response")]
      ChromeProtocol.Domains.CacheStorage.CachedResponseType Response
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Requests data from cache.</summary>
    /// <param name="CacheId">ID of cache to get entries from.</param>
    /// <param name="SkipCount">Number of records to skip.</param>
    /// <param name="PageSize">Number of records to fetch.</param>
    /// <param name="PathFilter">If present, only return the entries containing this substring in the path</param>
    public static ChromeProtocol.Domains.CacheStorage.RequestEntriesRequest RequestEntries(ChromeProtocol.Domains.CacheStorage.CacheIdType CacheId, int? SkipCount = default, int? PageSize = default, string? PathFilter = default)    
    {
      return new ChromeProtocol.Domains.CacheStorage.RequestEntriesRequest(CacheId, SkipCount, PageSize, PathFilter);
    }
    /// <summary>Requests data from cache.</summary>
    /// <param name="CacheId">ID of cache to get entries from.</param>
    /// <param name="SkipCount">Number of records to skip.</param>
    /// <param name="PageSize">Number of records to fetch.</param>
    /// <param name="PathFilter">If present, only return the entries containing this substring in the path</param>
    [ChromeProtocol.Core.MethodName("CacheStorage.requestEntries")]
    public record RequestEntriesRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("cacheId")]
      ChromeProtocol.Domains.CacheStorage.CacheIdType CacheId,
      [property: System.Text.Json.Serialization.JsonPropertyName("skipCount")]
      int? SkipCount = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("pageSize")]
      int? PageSize = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("pathFilter")]
      string? PathFilter = default
    ) : ChromeProtocol.Core.ICommand<RequestEntriesRequestResult>
    {
    }
    /// <param name="CacheDataEntries">Array of object store data entries.</param>
    /// <param name="ReturnCount">
    /// Count of returned entries from this storage. If pathFilter is empty, it<br/>
    /// is the count of all entries from this storage.<br/>
    /// </param>
    public record RequestEntriesRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("cacheDataEntries")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.CacheStorage.DataEntryType> CacheDataEntries,
      [property: System.Text.Json.Serialization.JsonPropertyName("returnCount")]
      double ReturnCount
    ) : ChromeProtocol.Core.IType
    {
    }
  }
}
