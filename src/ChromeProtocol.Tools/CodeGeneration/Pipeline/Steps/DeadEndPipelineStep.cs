namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Steps;

public class DeadEndPipelineStep<TContext> : ICodeGenerationPipelineStep<TContext>
{
  public Task InvokeAsync(CodeGenerationPipeline<TContext> next, TContext _) => Task.CompletedTask;
}
