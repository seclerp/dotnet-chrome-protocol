using ChromeProtocol.Tools.Schema.Models;

namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;

public record ValidatedType(
  string Id,
  string Description,
  TypeKind? Kind,
  ValidatedProperty[] Properties,
  string[] Enum,
  bool Experimental,
  bool Deprecated
);
