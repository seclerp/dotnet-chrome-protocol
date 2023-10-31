namespace ChromeProtocol.Tools.CodeGeneration.Pipeline;

/// <summary>
/// Delegate that represents invokable pipeline.
/// </summary>
/// <typeparam name="TContext">Type of context.</typeparam>
/// <param name="ctx">Instance of <see cref="TContext"/>.</param>
/// <returns>Instance of <see cref="Task"/>.</returns>
public delegate Task CodeGenerationPipeline<in TContext>(TContext ctx);
