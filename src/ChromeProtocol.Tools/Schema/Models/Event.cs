using Json.Schema.Generation;

namespace ChromeProtocol.Tools.Schema.Models;

public record Event(
  [property: Required]
  string Name,
  string? Description,
  Property[]? Parameters,
  bool? Experimental,
  bool? Deprecated
);
