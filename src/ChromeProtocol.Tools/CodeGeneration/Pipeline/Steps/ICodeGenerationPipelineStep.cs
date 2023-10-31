namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Steps;

// Func<EventContext, Task>

public interface ICodeGenerationPipelineStep<TContext>
{
  Task InvokeAsync(CodeGenerationPipeline<TContext> next, TContext ctx);
}

