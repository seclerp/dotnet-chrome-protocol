using ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;

namespace ChromeProtocol.Tools.CodeGeneration.Pipeline;

public class CodeGenerationContext
{
  public string OutputFolder { get; init; }
  public bool Clean { get; init; }
  public string Namespace { get; init; }
  public IEnumerable<string> DefinitionFiles { get; init; }

  public IReadOnlyCollection<DefinitionFile> ValidatedDefinitions { get; set; }
  public IReadOnlyCollection<ValidatedDomain>? MergedDomains { get; set; }
  public IReadOnlyCollection<(string, string)>? GeneratedSources { get; set; }
  public ICollection<CodeGenerationError> Errors { get; } = new LinkedList<CodeGenerationError>();
}

public record CodeGenerationError(string FileName, string Message);

