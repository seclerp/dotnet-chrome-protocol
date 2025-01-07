// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  /// <summary>A domain for letting clients substitute browser&#39;s network layer with client code.</summary>
  public static partial class Fetch
  {
    /// <summary>Unique request identifier.</summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record RequestIdType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <summary>
    /// Stages of the request to handle. Request will intercept before the request is<br/>
    /// sent. Response will intercept after the response is received (but before response<br/>
    /// body is received).<br/>
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(ChromeProtocol.Core.PrimitiveTypeConverter))]
    public record RequestStageType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <param name="UrlPattern">
    /// Wildcards (`&#39;*&#39;` -&gt; zero or more, `&#39;?&#39;` -&gt; exactly one) are allowed. Escape character is<br/>
    /// backslash. Omitting is equivalent to `&quot;*&quot;`.<br/>
    /// </param>
    /// <param name="ResourceType">If set, only requests for matching resource types will be intercepted.</param>
    /// <param name="RequestStage">Stage at which to begin intercepting requests. Default is Request.</param>
    public record RequestPatternType(
      [property: System.Text.Json.Serialization.JsonPropertyName("urlPattern")]
      string? UrlPattern = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("resourceType")]
      ChromeProtocol.Domains.Network.ResourceTypeType? ResourceType = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("requestStage")]
      ChromeProtocol.Domains.Fetch.RequestStageType? RequestStage = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Response HTTP header entry</summary>
    public record HeaderEntryType(
      [property: System.Text.Json.Serialization.JsonPropertyName("name")]
      string Name,
      [property: System.Text.Json.Serialization.JsonPropertyName("value")]
      string Value
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Authorization challenge for HTTP status code 401 or 407.</summary>
    /// <param name="Origin">Origin of the challenger.</param>
    /// <param name="Scheme">The authentication scheme used, such as basic or digest</param>
    /// <param name="Realm">The realm of the challenge. May be empty.</param>
    /// <param name="Source">Source of the authentication challenge.</param>
    public record AuthChallengeType(
      [property: System.Text.Json.Serialization.JsonPropertyName("origin")]
      string Origin,
      [property: System.Text.Json.Serialization.JsonPropertyName("scheme")]
      string Scheme,
      [property: System.Text.Json.Serialization.JsonPropertyName("realm")]
      string Realm,
      [property: System.Text.Json.Serialization.JsonPropertyName("source")]
      string? Source = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Response to an AuthChallenge.</summary>
    /// <param name="Response">
    /// The decision on what to do in response to the authorization challenge.  Default means<br/>
    /// deferring to the default behavior of the net stack, which will likely either the Cancel<br/>
    /// authentication or display a popup dialog box.<br/>
    /// </param>
    /// <param name="Username">
    /// The username to provide, possibly empty. Should only be set if response is<br/>
    /// ProvideCredentials.<br/>
    /// </param>
    /// <param name="Password">
    /// The password to provide, possibly empty. Should only be set if response is<br/>
    /// ProvideCredentials.<br/>
    /// </param>
    public record AuthChallengeResponseType(
      [property: System.Text.Json.Serialization.JsonPropertyName("response")]
      string Response,
      [property: System.Text.Json.Serialization.JsonPropertyName("username")]
      string? Username = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("password")]
      string? Password = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>
    /// Issued when the domain is enabled and the request URL matches the<br/>
    /// specified filter. The request is paused until the client responds<br/>
    /// with one of continueRequest, failRequest or fulfillRequest.<br/>
    /// The stage of the request can be determined by presence of responseErrorReason<br/>
    /// and responseStatusCode -- the request is at the response stage if either<br/>
    /// of these fields is present and in the request stage otherwise.<br/>
    /// Redirect responses and subsequent requests are reported similarly to regular<br/>
    /// responses and requests. Redirect responses may be distinguished by the value<br/>
    /// of `responseStatusCode` (which is one of 301, 302, 303, 307, 308) along with<br/>
    /// presence of the `location` header. Requests resulting from a redirect will<br/>
    /// have `redirectedRequestId` field set.<br/>
    /// </summary>
    /// <param name="RequestId">Each request the page makes will have a unique id.</param>
    /// <param name="Request">The details of the request.</param>
    /// <param name="FrameId">The id of the frame that initiated the request.</param>
    /// <param name="ResourceType">How the requested resource will be used.</param>
    /// <param name="ResponseErrorReason">Response error if intercepted at response stage.</param>
    /// <param name="ResponseStatusCode">Response code if intercepted at response stage.</param>
    /// <param name="ResponseStatusText">Response status text if intercepted at response stage.</param>
    /// <param name="ResponseHeaders">Response headers if intercepted at the response stage.</param>
    /// <param name="NetworkId">
    /// If the intercepted request had a corresponding Network.requestWillBeSent event fired for it,<br/>
    /// then this networkId will be the same as the requestId present in the requestWillBeSent event.<br/>
    /// </param>
    /// <param name="RedirectedRequestId">
    /// If the request is due to a redirect response from the server, the id of the request that<br/>
    /// has caused the redirect.<br/>
    /// </param>
    [ChromeProtocol.Core.MethodName("Fetch.requestPaused")]
    public record RequestPaused(
      [property: System.Text.Json.Serialization.JsonPropertyName("requestId")]
      ChromeProtocol.Domains.Fetch.RequestIdType RequestId,
      [property: System.Text.Json.Serialization.JsonPropertyName("request")]
      ChromeProtocol.Domains.Network.RequestType Request,
      [property: System.Text.Json.Serialization.JsonPropertyName("frameId")]
      ChromeProtocol.Domains.Page.FrameIdType FrameId,
      [property: System.Text.Json.Serialization.JsonPropertyName("resourceType")]
      ChromeProtocol.Domains.Network.ResourceTypeType ResourceType,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseErrorReason")]
      ChromeProtocol.Domains.Network.ErrorReasonType? ResponseErrorReason = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseStatusCode")]
      int? ResponseStatusCode = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseStatusText")]
      string? ResponseStatusText = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseHeaders")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Fetch.HeaderEntryType>? ResponseHeaders = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("networkId")]
      ChromeProtocol.Domains.Network.RequestIdType? NetworkId = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("redirectedRequestId")]
      ChromeProtocol.Domains.Fetch.RequestIdType? RedirectedRequestId = default
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>
    /// Issued when the domain is enabled with handleAuthRequests set to true.<br/>
    /// The request is paused until client responds with continueWithAuth.<br/>
    /// </summary>
    /// <param name="RequestId">Each request the page makes will have a unique id.</param>
    /// <param name="Request">The details of the request.</param>
    /// <param name="FrameId">The id of the frame that initiated the request.</param>
    /// <param name="ResourceType">How the requested resource will be used.</param>
    /// <param name="AuthChallenge">
    /// Details of the Authorization Challenge encountered.<br/>
    /// If this is set, client should respond with continueRequest that<br/>
    /// contains AuthChallengeResponse.<br/>
    /// </param>
    [ChromeProtocol.Core.MethodName("Fetch.authRequired")]
    public record AuthRequired(
      [property: System.Text.Json.Serialization.JsonPropertyName("requestId")]
      ChromeProtocol.Domains.Fetch.RequestIdType RequestId,
      [property: System.Text.Json.Serialization.JsonPropertyName("request")]
      ChromeProtocol.Domains.Network.RequestType Request,
      [property: System.Text.Json.Serialization.JsonPropertyName("frameId")]
      ChromeProtocol.Domains.Page.FrameIdType FrameId,
      [property: System.Text.Json.Serialization.JsonPropertyName("resourceType")]
      ChromeProtocol.Domains.Network.ResourceTypeType ResourceType,
      [property: System.Text.Json.Serialization.JsonPropertyName("authChallenge")]
      ChromeProtocol.Domains.Fetch.AuthChallengeType AuthChallenge
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Disables the fetch domain.</summary>
    public static ChromeProtocol.Domains.Fetch.DisableRequest Disable()    
    {
      return new ChromeProtocol.Domains.Fetch.DisableRequest();
    }
    /// <summary>Disables the fetch domain.</summary>
    [ChromeProtocol.Core.MethodName("Fetch.disable")]
    public record DisableRequest() : ChromeProtocol.Core.ICommand<DisableRequestResult>
    {
    }
    public record DisableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>
    /// Enables issuing of requestPaused events. A request will be paused until client<br/>
    /// calls one of failRequest, fulfillRequest or continueRequest/continueWithAuth.<br/>
    /// </summary>
    /// <param name="Patterns">
    /// If specified, only requests matching any of these patterns will produce<br/>
    /// fetchRequested event and will be paused until clients response. If not set,<br/>
    /// all requests will be affected.<br/>
    /// </param>
    /// <param name="HandleAuthRequests">
    /// If true, authRequired events will be issued and requests will be paused<br/>
    /// expecting a call to continueWithAuth.<br/>
    /// </param>
    public static ChromeProtocol.Domains.Fetch.EnableRequest Enable(System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Fetch.RequestPatternType>? Patterns = default, bool? HandleAuthRequests = default)    
    {
      return new ChromeProtocol.Domains.Fetch.EnableRequest(Patterns, HandleAuthRequests);
    }
    /// <summary>
    /// Enables issuing of requestPaused events. A request will be paused until client<br/>
    /// calls one of failRequest, fulfillRequest or continueRequest/continueWithAuth.<br/>
    /// </summary>
    /// <param name="Patterns">
    /// If specified, only requests matching any of these patterns will produce<br/>
    /// fetchRequested event and will be paused until clients response. If not set,<br/>
    /// all requests will be affected.<br/>
    /// </param>
    /// <param name="HandleAuthRequests">
    /// If true, authRequired events will be issued and requests will be paused<br/>
    /// expecting a call to continueWithAuth.<br/>
    /// </param>
    [ChromeProtocol.Core.MethodName("Fetch.enable")]
    public record EnableRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("patterns")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Fetch.RequestPatternType>? Patterns = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("handleAuthRequests")]
      bool? HandleAuthRequests = default
    ) : ChromeProtocol.Core.ICommand<EnableRequestResult>
    {
    }
    public record EnableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Causes the request to fail with specified reason.</summary>
    /// <param name="RequestId">An id the client received in requestPaused event.</param>
    /// <param name="ErrorReason">Causes the request to fail with the given reason.</param>
    public static ChromeProtocol.Domains.Fetch.FailRequestRequest FailRequest(ChromeProtocol.Domains.Fetch.RequestIdType RequestId, ChromeProtocol.Domains.Network.ErrorReasonType ErrorReason)    
    {
      return new ChromeProtocol.Domains.Fetch.FailRequestRequest(RequestId, ErrorReason);
    }
    /// <summary>Causes the request to fail with specified reason.</summary>
    /// <param name="RequestId">An id the client received in requestPaused event.</param>
    /// <param name="ErrorReason">Causes the request to fail with the given reason.</param>
    [ChromeProtocol.Core.MethodName("Fetch.failRequest")]
    public record FailRequestRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("requestId")]
      ChromeProtocol.Domains.Fetch.RequestIdType RequestId,
      [property: System.Text.Json.Serialization.JsonPropertyName("errorReason")]
      ChromeProtocol.Domains.Network.ErrorReasonType ErrorReason
    ) : ChromeProtocol.Core.ICommand<FailRequestRequestResult>
    {
    }
    public record FailRequestRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Provides response to the request.</summary>
    /// <param name="RequestId">An id the client received in requestPaused event.</param>
    /// <param name="ResponseCode">An HTTP response code.</param>
    /// <param name="ResponseHeaders">Response headers.</param>
    /// <param name="BinaryResponseHeaders">
    /// Alternative way of specifying response headers as a \0-separated<br/>
    /// series of name: value pairs. Prefer the above method unless you<br/>
    /// need to represent some non-UTF8 values that can&#39;t be transmitted<br/>
    /// over the protocol as text. (Encoded as a base64 string when passed over JSON)<br/>
    /// </param>
    /// <param name="Body">
    /// A response body. If absent, original response body will be used if<br/>
    /// the request is intercepted at the response stage and empty body<br/>
    /// will be used if the request is intercepted at the request stage. (Encoded as a base64 string when passed over JSON)<br/>
    /// </param>
    /// <param name="ResponsePhrase">
    /// A textual representation of responseCode.<br/>
    /// If absent, a standard phrase matching responseCode is used.<br/>
    /// </param>
    public static ChromeProtocol.Domains.Fetch.FulfillRequestRequest FulfillRequest(ChromeProtocol.Domains.Fetch.RequestIdType RequestId, int ResponseCode, System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Fetch.HeaderEntryType>? ResponseHeaders = default, string? BinaryResponseHeaders = default, string? Body = default, string? ResponsePhrase = default)    
    {
      return new ChromeProtocol.Domains.Fetch.FulfillRequestRequest(RequestId, ResponseCode, ResponseHeaders, BinaryResponseHeaders, Body, ResponsePhrase);
    }
    /// <summary>Provides response to the request.</summary>
    /// <param name="RequestId">An id the client received in requestPaused event.</param>
    /// <param name="ResponseCode">An HTTP response code.</param>
    /// <param name="ResponseHeaders">Response headers.</param>
    /// <param name="BinaryResponseHeaders">
    /// Alternative way of specifying response headers as a \0-separated<br/>
    /// series of name: value pairs. Prefer the above method unless you<br/>
    /// need to represent some non-UTF8 values that can&#39;t be transmitted<br/>
    /// over the protocol as text. (Encoded as a base64 string when passed over JSON)<br/>
    /// </param>
    /// <param name="Body">
    /// A response body. If absent, original response body will be used if<br/>
    /// the request is intercepted at the response stage and empty body<br/>
    /// will be used if the request is intercepted at the request stage. (Encoded as a base64 string when passed over JSON)<br/>
    /// </param>
    /// <param name="ResponsePhrase">
    /// A textual representation of responseCode.<br/>
    /// If absent, a standard phrase matching responseCode is used.<br/>
    /// </param>
    [ChromeProtocol.Core.MethodName("Fetch.fulfillRequest")]
    public record FulfillRequestRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("requestId")]
      ChromeProtocol.Domains.Fetch.RequestIdType RequestId,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseCode")]
      int ResponseCode,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseHeaders")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Fetch.HeaderEntryType>? ResponseHeaders = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("binaryResponseHeaders")]
      string? BinaryResponseHeaders = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("body")]
      string? Body = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("responsePhrase")]
      string? ResponsePhrase = default
    ) : ChromeProtocol.Core.ICommand<FulfillRequestRequestResult>
    {
    }
    public record FulfillRequestRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Continues the request, optionally modifying some of its parameters.</summary>
    /// <param name="RequestId">An id the client received in requestPaused event.</param>
    /// <param name="Url">If set, the request url will be modified in a way that&#39;s not observable by page.</param>
    /// <param name="Method">If set, the request method is overridden.</param>
    /// <param name="PostData">If set, overrides the post data in the request. (Encoded as a base64 string when passed over JSON)</param>
    /// <param name="Headers">
    /// If set, overrides the request headers. Note that the overrides do not<br/>
    /// extend to subsequent redirect hops, if a redirect happens. Another override<br/>
    /// may be applied to a different request produced by a redirect.<br/>
    /// </param>
    /// <param name="InterceptResponse">If set, overrides response interception behavior for this request.</param>
    public static ChromeProtocol.Domains.Fetch.ContinueRequestRequest ContinueRequest(ChromeProtocol.Domains.Fetch.RequestIdType RequestId, string? Url = default, string? Method = default, string? PostData = default, System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Fetch.HeaderEntryType>? Headers = default, bool? InterceptResponse = default)    
    {
      return new ChromeProtocol.Domains.Fetch.ContinueRequestRequest(RequestId, Url, Method, PostData, Headers, InterceptResponse);
    }
    /// <summary>Continues the request, optionally modifying some of its parameters.</summary>
    /// <param name="RequestId">An id the client received in requestPaused event.</param>
    /// <param name="Url">If set, the request url will be modified in a way that&#39;s not observable by page.</param>
    /// <param name="Method">If set, the request method is overridden.</param>
    /// <param name="PostData">If set, overrides the post data in the request. (Encoded as a base64 string when passed over JSON)</param>
    /// <param name="Headers">
    /// If set, overrides the request headers. Note that the overrides do not<br/>
    /// extend to subsequent redirect hops, if a redirect happens. Another override<br/>
    /// may be applied to a different request produced by a redirect.<br/>
    /// </param>
    /// <param name="InterceptResponse">If set, overrides response interception behavior for this request.</param>
    [ChromeProtocol.Core.MethodName("Fetch.continueRequest")]
    public record ContinueRequestRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("requestId")]
      ChromeProtocol.Domains.Fetch.RequestIdType RequestId,
      [property: System.Text.Json.Serialization.JsonPropertyName("url")]
      string? Url = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("method")]
      string? Method = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("postData")]
      string? PostData = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("headers")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Fetch.HeaderEntryType>? Headers = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("interceptResponse")]
      bool? InterceptResponse = default
    ) : ChromeProtocol.Core.ICommand<ContinueRequestRequestResult>
    {
    }
    public record ContinueRequestRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Continues a request supplying authChallengeResponse following authRequired event.</summary>
    /// <param name="RequestId">An id the client received in authRequired event.</param>
    /// <param name="AuthChallengeResponse">Response to  with an authChallenge.</param>
    public static ChromeProtocol.Domains.Fetch.ContinueWithAuthRequest ContinueWithAuth(ChromeProtocol.Domains.Fetch.RequestIdType RequestId, ChromeProtocol.Domains.Fetch.AuthChallengeResponseType AuthChallengeResponse)    
    {
      return new ChromeProtocol.Domains.Fetch.ContinueWithAuthRequest(RequestId, AuthChallengeResponse);
    }
    /// <summary>Continues a request supplying authChallengeResponse following authRequired event.</summary>
    /// <param name="RequestId">An id the client received in authRequired event.</param>
    /// <param name="AuthChallengeResponse">Response to  with an authChallenge.</param>
    [ChromeProtocol.Core.MethodName("Fetch.continueWithAuth")]
    public record ContinueWithAuthRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("requestId")]
      ChromeProtocol.Domains.Fetch.RequestIdType RequestId,
      [property: System.Text.Json.Serialization.JsonPropertyName("authChallengeResponse")]
      ChromeProtocol.Domains.Fetch.AuthChallengeResponseType AuthChallengeResponse
    ) : ChromeProtocol.Core.ICommand<ContinueWithAuthRequestResult>
    {
    }
    public record ContinueWithAuthRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>
    /// Continues loading of the paused response, optionally modifying the<br/>
    /// response headers. If either responseCode or headers are modified, all of them<br/>
    /// must be present.<br/>
    /// </summary>
    /// <param name="RequestId">An id the client received in requestPaused event.</param>
    /// <param name="ResponseCode">An HTTP response code. If absent, original response code will be used.</param>
    /// <param name="ResponsePhrase">
    /// A textual representation of responseCode.<br/>
    /// If absent, a standard phrase matching responseCode is used.<br/>
    /// </param>
    /// <param name="ResponseHeaders">Response headers. If absent, original response headers will be used.</param>
    /// <param name="BinaryResponseHeaders">
    /// Alternative way of specifying response headers as a \0-separated<br/>
    /// series of name: value pairs. Prefer the above method unless you<br/>
    /// need to represent some non-UTF8 values that can&#39;t be transmitted<br/>
    /// over the protocol as text. (Encoded as a base64 string when passed over JSON)<br/>
    /// </param>
    public static ChromeProtocol.Domains.Fetch.ContinueResponseRequest ContinueResponse(ChromeProtocol.Domains.Fetch.RequestIdType RequestId, int? ResponseCode = default, string? ResponsePhrase = default, System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Fetch.HeaderEntryType>? ResponseHeaders = default, string? BinaryResponseHeaders = default)    
    {
      return new ChromeProtocol.Domains.Fetch.ContinueResponseRequest(RequestId, ResponseCode, ResponsePhrase, ResponseHeaders, BinaryResponseHeaders);
    }
    /// <summary>
    /// Continues loading of the paused response, optionally modifying the<br/>
    /// response headers. If either responseCode or headers are modified, all of them<br/>
    /// must be present.<br/>
    /// </summary>
    /// <param name="RequestId">An id the client received in requestPaused event.</param>
    /// <param name="ResponseCode">An HTTP response code. If absent, original response code will be used.</param>
    /// <param name="ResponsePhrase">
    /// A textual representation of responseCode.<br/>
    /// If absent, a standard phrase matching responseCode is used.<br/>
    /// </param>
    /// <param name="ResponseHeaders">Response headers. If absent, original response headers will be used.</param>
    /// <param name="BinaryResponseHeaders">
    /// Alternative way of specifying response headers as a \0-separated<br/>
    /// series of name: value pairs. Prefer the above method unless you<br/>
    /// need to represent some non-UTF8 values that can&#39;t be transmitted<br/>
    /// over the protocol as text. (Encoded as a base64 string when passed over JSON)<br/>
    /// </param>
    [ChromeProtocol.Core.MethodName("Fetch.continueResponse")]
    public record ContinueResponseRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("requestId")]
      ChromeProtocol.Domains.Fetch.RequestIdType RequestId,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseCode")]
      int? ResponseCode = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("responsePhrase")]
      string? ResponsePhrase = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("responseHeaders")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Fetch.HeaderEntryType>? ResponseHeaders = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("binaryResponseHeaders")]
      string? BinaryResponseHeaders = default
    ) : ChromeProtocol.Core.ICommand<ContinueResponseRequestResult>
    {
    }
    public record ContinueResponseRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>
    /// Causes the body of the response to be received from the server and<br/>
    /// returned as a single string. May only be issued for a request that<br/>
    /// is paused in the Response stage and is mutually exclusive with<br/>
    /// takeResponseBodyForInterceptionAsStream. Calling other methods that<br/>
    /// affect the request or disabling fetch domain before body is received<br/>
    /// results in an undefined behavior.<br/>
    /// Note that the response body is not available for redirects. Requests<br/>
    /// paused in the _redirect received_ state may be differentiated by<br/>
    /// `responseCode` and presence of `location` response header, see<br/>
    /// comments to `requestPaused` for details.<br/>
    /// </summary>
    /// <param name="RequestId">Identifier for the intercepted request to get body for.</param>
    public static ChromeProtocol.Domains.Fetch.GetResponseBodyRequest GetResponseBody(ChromeProtocol.Domains.Fetch.RequestIdType RequestId)    
    {
      return new ChromeProtocol.Domains.Fetch.GetResponseBodyRequest(RequestId);
    }
    /// <summary>
    /// Causes the body of the response to be received from the server and<br/>
    /// returned as a single string. May only be issued for a request that<br/>
    /// is paused in the Response stage and is mutually exclusive with<br/>
    /// takeResponseBodyForInterceptionAsStream. Calling other methods that<br/>
    /// affect the request or disabling fetch domain before body is received<br/>
    /// results in an undefined behavior.<br/>
    /// Note that the response body is not available for redirects. Requests<br/>
    /// paused in the _redirect received_ state may be differentiated by<br/>
    /// `responseCode` and presence of `location` response header, see<br/>
    /// comments to `requestPaused` for details.<br/>
    /// </summary>
    /// <param name="RequestId">Identifier for the intercepted request to get body for.</param>
    [ChromeProtocol.Core.MethodName("Fetch.getResponseBody")]
    public record GetResponseBodyRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("requestId")]
      ChromeProtocol.Domains.Fetch.RequestIdType RequestId
    ) : ChromeProtocol.Core.ICommand<GetResponseBodyRequestResult>
    {
    }
    /// <param name="Body">Response body.</param>
    /// <param name="Base64Encoded">True, if content was sent as base64.</param>
    public record GetResponseBodyRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("body")]
      string Body,
      [property: System.Text.Json.Serialization.JsonPropertyName("base64Encoded")]
      bool Base64Encoded
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>
    /// Returns a handle to the stream representing the response body.<br/>
    /// The request must be paused in the HeadersReceived stage.<br/>
    /// Note that after this command the request can&#39;t be continued<br/>
    /// as is -- client either needs to cancel it or to provide the<br/>
    /// response body.<br/>
    /// The stream only supports sequential read, IO.read will fail if the position<br/>
    /// is specified.<br/>
    /// This method is mutually exclusive with getResponseBody.<br/>
    /// Calling other methods that affect the request or disabling fetch<br/>
    /// domain before body is received results in an undefined behavior.<br/>
    /// </summary>
    public static ChromeProtocol.Domains.Fetch.TakeResponseBodyAsStreamRequest TakeResponseBodyAsStream(ChromeProtocol.Domains.Fetch.RequestIdType RequestId)    
    {
      return new ChromeProtocol.Domains.Fetch.TakeResponseBodyAsStreamRequest(RequestId);
    }
    /// <summary>
    /// Returns a handle to the stream representing the response body.<br/>
    /// The request must be paused in the HeadersReceived stage.<br/>
    /// Note that after this command the request can&#39;t be continued<br/>
    /// as is -- client either needs to cancel it or to provide the<br/>
    /// response body.<br/>
    /// The stream only supports sequential read, IO.read will fail if the position<br/>
    /// is specified.<br/>
    /// This method is mutually exclusive with getResponseBody.<br/>
    /// Calling other methods that affect the request or disabling fetch<br/>
    /// domain before body is received results in an undefined behavior.<br/>
    /// </summary>
    [ChromeProtocol.Core.MethodName("Fetch.takeResponseBodyAsStream")]
    public record TakeResponseBodyAsStreamRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("requestId")]
      ChromeProtocol.Domains.Fetch.RequestIdType RequestId
    ) : ChromeProtocol.Core.ICommand<TakeResponseBodyAsStreamRequestResult>
    {
    }
    public record TakeResponseBodyAsStreamRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("stream")]
      ChromeProtocol.Domains.IO.StreamHandleType Stream
    ) : ChromeProtocol.Core.IType
    {
    }
  }
}
