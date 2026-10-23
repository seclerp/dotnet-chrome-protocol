using System.Reflection;
using System.Text.Json;
using ChromeProtocol.Tools.Logging;
using Json.Schema;

namespace ChromeProtocol.Tools.Services;

public class ProtocolSchemaValidator
{
  private readonly SpectreErrorFormatter _formatter = new();
  public async Task<(bool, IEnumerable<string>)> TryValidateAsync(string fileName, JsonDocument protocolDocument)
  {
    await using var schemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ChromeProtocol.Tools.protocol_definition_schema.json");
    using var schemaDocument = await JsonDocument.ParseAsync(schemaStream).ConfigureAwait(false);
    var schema = JsonSchema.Build(schemaDocument.RootElement);
    var result = schema.Evaluate(protocolDocument.RootElement, new EvaluationOptions
    {
      OutputFormat = OutputFormat.Hierarchical
    });
    if (!result.IsValid)
    {
      var presentableErrors = FlattenResults(result)
        .Where(results => results.Errors is { Count: > 0 })
        .Select(error => _formatter.Format(fileName, error));
      return (false, presentableErrors);
    }

    return (true, Enumerable.Empty<string>());
  }

  private IEnumerable<EvaluationResults> FlattenResults(EvaluationResults root)
  {
    void CollectResults(EvaluationResults node, LinkedList<EvaluationResults> acc)
    {
      acc.AddLast(node);
      if (node.Details is { Count: > 0 })
      {
        foreach (var nested in node.Details)
        {
          CollectResults(nested, acc);
        }
      }
    }

    var acc = new LinkedList<EvaluationResults>();
    CollectResults(root, acc);
    return acc;
  }
}
