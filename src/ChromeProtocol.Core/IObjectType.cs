using System.Text.Json.Nodes;

namespace ChromeProtocol.Core;

public interface IObjectType
{
  public IReadOnlyDictionary<string, JsonNode?> Properties { get; }
}
