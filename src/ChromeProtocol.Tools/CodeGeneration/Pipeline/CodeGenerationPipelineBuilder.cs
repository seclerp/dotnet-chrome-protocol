using ChromeProtocol.Tools.CodeGeneration.Pipeline.Steps;

namespace ChromeProtocol.Tools.CodeGeneration.Pipeline;

/// <summary>
/// Instance-based interceptor pipeline.
/// </summary>
public class CodeGenerationPipelineBuilder<TContext>
{
  private readonly LinkedList<ICodeGenerationPipelineStep<TContext>> _interceptorsList;

  public CodeGenerationPipelineBuilder()
  {
    _interceptorsList = new LinkedList<ICodeGenerationPipelineStep<TContext>>();
  }

  public CodeGenerationPipelineBuilder<TContext> AddStep(ICodeGenerationPipelineStep<TContext> pipelineStep)
  {
    _interceptorsList.AddFirst(pipelineStep);

    return this;
  }

  public CodeGenerationPipeline<TContext> Build()
  {
    var currentStep = CreateStep(new DeadEndPipelineStep<TContext>(), _ => Task.CompletedTask);

    foreach (var interceptor in _interceptorsList)
    {
      var nextStep = currentStep;
      currentStep = CreateStep(interceptor, nextStep);
    }

    return currentStep;
  }

  private CodeGenerationPipeline<TContext> CreateStep(ICodeGenerationPipelineStep<TContext> pipelineStep, CodeGenerationPipeline<TContext> next) =>
    async ctx =>
    {
      await pipelineStep.InvokeAsync(next, ctx).ConfigureAwait(false);
    };
}
