using System.Text.Json.Nodes;

namespace ChromeProtocol.Core;

/// <summary>
/// An interface that represents JSON object in CDP.
/// </summary>
public interface IObjectType
{
  /// <summary>
  /// A property containing object's root properties as JSON objects.
  /// </summary>
  public IReadOnlyDictionary<string, JsonNode?> Properties { get; }
}
