namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;

public record ValidatedCommand(
  string Name,
  string Description,
  ValidatedProperty[] Parameters,
  ValidatedProperty[] Returns,
  bool Experimental,
  bool Deprecated
);
