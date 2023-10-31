using Version = ChromeProtocol.Tools.Schema.Models.Version;

namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;

public record ValidatedDefinition(
  Version Version,
  ValidatedDomain[] Domains
);
