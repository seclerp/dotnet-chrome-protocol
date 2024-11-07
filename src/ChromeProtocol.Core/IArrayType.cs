using System.Text.Json.Nodes;

namespace ChromeProtocol.Core;

public interface IArrayType
{
  public IReadOnlyCollection<JsonNode?> Items { get; }
}
