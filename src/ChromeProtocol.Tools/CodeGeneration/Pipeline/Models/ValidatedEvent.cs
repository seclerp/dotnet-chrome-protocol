namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;

public record ValidatedEvent(
  string Name,
  string Description,
  ValidatedProperty[] Parameters,
  bool Experimental,
  bool Deprecated
);
