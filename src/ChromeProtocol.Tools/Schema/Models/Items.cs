using System.Text.Json.Serialization;
using Json.Schema.Generation;

namespace ChromeProtocol.Tools.Schema.Models;

public record Items(
  [property: Required]
  TypeKind Type,
  [property: JsonPropertyName("$ref")]
  string? Ref
);
