using System.Text.Json.Nodes;

namespace ChromeProtocol.Core;

/// <summary>
/// An interface that represents generic array of JSON objects in CDP.
/// </summary>
public interface IArrayType
{
  /// <summary>
  /// A property containing array's items as JSON objects.
  /// </summary>
  public IReadOnlyCollection<JsonNode?> Items { get; }
}
