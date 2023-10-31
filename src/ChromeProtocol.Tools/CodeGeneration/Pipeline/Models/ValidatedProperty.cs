using ChromeProtocol.Tools.Schema.Models;

namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;

public record ValidatedProperty(
  string Name,
  string Description,
  string? Ref,
  bool Optional,
  TypeKind? Kind,
  string[] Enum,
  ValidatedItems? Items,
  bool Experimental,
  bool Deprecated
);
