namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class CsharpNamespaceDecl : IEmittable
{
  public string FullName { get; set; }

  public ICollection<CsharpTypeDecl> Classes { get; set; } = new LinkedList<CsharpTypeDecl>();

  public CsharpComment? Comment { get; set; }

  public void Emit(SourceCodeEmitter emitter, EmissionState state)
  {
    Comment?.Emit(emitter, state);
    emitter.EmitLineIdented($"namespace {FullName}", ref state);
    emitter.EmitLineIdented("{", ref state);

    foreach (var classDecl in Classes)
    {
      classDecl.Emit(emitter, state.WithIncreasedLevel());
    }

    emitter.EmitLineIdented("}", ref state);
  }
}
