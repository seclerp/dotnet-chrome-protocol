namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public interface IEmittable
{
  void Emit(SourceCodeEmitter emitter, EmissionState state);
}
