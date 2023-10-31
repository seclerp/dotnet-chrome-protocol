using System.ComponentModel;
using ChromeProtocol.Tools.CodeGeneration.Pipeline;
using ChromeProtocol.Tools.CodeGeneration.Pipeline.Steps;
using ChromeProtocol.Tools.Logging;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

namespace ChromeProtocol.Tools.Commands;

public sealed class GenerateCommand : AsyncCommand<GenerateCommand.Settings>
{
  private readonly ILogger _logger;
  private readonly IErrorFormatter _errorFormatter;

  public sealed class Settings : CommandSettings
  {
    [Description("Path to the .json file with the CDP schema to generate protocol definitions from.")]
    [CommandArgument(0, "<path>")]
    public string[] InputFiles { get; set; }

    [Description("Namespace for the generated files.")]
    [CommandOption("-n|--namespace")]
    [DefaultValue("Generated")]
    public string Namespace { get; set; }

    [Description("Folder where generated files should be placed.")]
    [CommandOption("-o|--output")]
    [DefaultValue("Generated")]
    public string OutputFolder { get; set; }

    [Description("Should output folder be cleaned before performing generation or not.")]
    [CommandOption("--clean")]
    [DefaultValue(true)]
    public bool Clean { get; set; }
  }

  private readonly CodeGenerationPipeline<CodeGenerationContext> _pipeline;

  public GenerateCommand(ILogger logger, IErrorFormatter errorFormatter)
  {
    _logger = logger;
    _errorFormatter = errorFormatter;
    _pipeline = new CodeGenerationPipelineBuilder<CodeGenerationContext>()
      .AddStep(new DefinitionFilesValidationStep())
      .AddStep(new MergeDefinitionsStep())
      .AddStep(new GenerateCsharpStep())
      .AddStep(new OutputProducingStep())
      .Build();
  }

  public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
  {
    var ctx = new CodeGenerationContext
    {
      OutputFolder = NormalizePath(settings.OutputFolder),
      Clean = settings.Clean,
      Namespace = settings.Namespace,
      DefinitionFiles = settings.InputFiles.Select(NormalizePath)
    };
    await _pipeline.Invoke(ctx).ConfigureAwait(false);

    if (ctx.Errors.Any())
    {
      foreach (var error in ctx.Errors)
      {
        var formattedError = _errorFormatter.Format(error);
        _logger.LogError(formattedError);
      }
      return -1;
    }
    return 0;
  }

  private static string NormalizePath(string path)
  {
    if (!Path.IsPathRooted(path) && !path.StartsWith("~/"))
    {
      return Path.GetFullPath(path, Directory.GetCurrentDirectory());
    }

    return path;
  }
}
