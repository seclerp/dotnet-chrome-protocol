using Json.Schema.Generation;

namespace ChromeProtocol.Tools.Schema.Models;

public record Definition(
  [property: Required]
  Version Version,
  [property: Required]
  Domain[] Domains
);
