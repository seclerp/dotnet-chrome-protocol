namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;

public record ValidatedDomain(
  string Name,
  string Description,
  string[] Dependencies,
  ValidatedType[] Types,
  ValidatedEvent[] Events,
  ValidatedCommand[] Commands,
  bool Experimental,
  bool Deprecated
);
