using Json.Schema.Generation;

namespace ChromeProtocol.Tools.Schema.Models;

public record Version(
  [property: Required]
  string Major,
  [property: Required]
  string Minor
);
