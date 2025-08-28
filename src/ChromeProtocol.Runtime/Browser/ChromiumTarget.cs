using System.Text.Json.Serialization;

namespace ChromeProtocol.Runtime.Browser;

/// <summary>
/// Represents a single DevTools target obtained from the /json endpoint.
/// </summary>
public record ChromiumTarget(
  [property: JsonPropertyName("id")] string Id,
  [property: JsonPropertyName("type")] string Type,
  [property: JsonPropertyName("title")] string? Title,
  [property: JsonPropertyName("url")] string? Url,
  [property: JsonPropertyName("webSocketDebuggerUrl")] string? WebSocketDebuggerUrl
);
