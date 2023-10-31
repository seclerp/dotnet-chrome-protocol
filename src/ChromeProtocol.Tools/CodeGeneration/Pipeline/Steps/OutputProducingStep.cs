namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Steps;

public class OutputProducingStep : ICodeGenerationPipelineStep<CodeGenerationContext>
{
  public async Task InvokeAsync(CodeGenerationPipeline<CodeGenerationContext> next, CodeGenerationContext ctx)
  {
    if (ctx.GeneratedSources is null) return;

    if (ctx.Clean)
    {
      var outputFolderInfo = new DirectoryInfo(ctx.OutputFolder);
      foreach (var file in outputFolderInfo.GetFiles())
      {
        file.Delete();
      }
      foreach (var dir in outputFolderInfo.GetDirectories())
      {
        dir.Delete(true);
      }
    }

    await Task.WhenAll(ctx.GeneratedSources.Select(async kv =>
    {
      var fileName = Path.Combine(ctx.OutputFolder, $"{kv.Item1}.cs");
      if (File.Exists(fileName))
      {
        File.Delete(fileName);
      }
      await File.WriteAllTextAsync(fileName, kv.Item2).ConfigureAwait(false);
    })).ConfigureAwait(false);

    await next.Invoke(ctx).ConfigureAwait(false);
  }
}
