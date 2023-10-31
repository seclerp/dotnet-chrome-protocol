using ChromeProtocol.Tools.Schema.Models;

namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;

public record ValidatedItems(
  TypeKind Type,
  string? Ref
);
