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
    var schema = await JsonSchema.FromStream(schemaStream).ConfigureAwait(false);
    var result = schema.Validate(protocolDocument, new ValidationOptions
    {
      OutputFormat = OutputFormat.Detailed
    });
    if (!result.IsValid)
    {
      var presentableErrors = FlattenResults(result)
        .Where(results => results.Message != null)
        .Select(error => _formatter.Format(fileName, error));
      return (false, presentableErrors);
    }

    return (true, Enumerable.Empty<string>());
  }

  private IEnumerable<ValidationResults> FlattenResults(ValidationResults root)
  {
    void CollectResults(ValidationResults node, LinkedList<ValidationResults> acc)
    {
      node.ToDetailed();
      acc.AddLast(node);
      if (node.HasNestedResults)
      {
        foreach (var nested in node.NestedResults)
        {
          CollectResults(nested, acc);
        }
      }
    }

    var acc = new LinkedList<ValidationResults>();
    CollectResults(root, acc);
    return acc;
  }
}
