using System.Text.Json.Serialization;
using Json.Schema.Generation;

namespace ChromeProtocol.Tools.Schema.Models;

public record Property(
  [property: Required]
  string Name,
  string? Description,
  [property: JsonPropertyName("$ref")]
  string? Ref,
  bool? Optional,
  [property: JsonPropertyName("type")]
  TypeKind? Kind,
  string[]? Enum,
  Items? Items,
  bool? Experimental,
  bool? Deprecated
);
