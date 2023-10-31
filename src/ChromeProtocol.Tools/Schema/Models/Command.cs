using Json.Schema.Generation;

namespace ChromeProtocol.Tools.Schema.Models;

public record Command(
  [property: Required]
  string Name,
  string? Description,
  Property[]? Parameters,
  Property[]? Returns,
  bool? Experimental,
  bool? Deprecated
);
