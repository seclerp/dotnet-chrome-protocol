using System.Text.Json.Serialization;

namespace ChromeProtocol.Tools.Schema.Models;

public record Domain(
  [property: JsonPropertyName("domain")]
  string Name,
  string? Description,
  string[]? Dependencies,
  Type[]? Types,
  Event[]? Events,
  Command[]? Commands,
  bool? Experimental,
  bool? Deprecated
);
