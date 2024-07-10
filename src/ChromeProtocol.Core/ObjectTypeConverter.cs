using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace ChromeProtocol.Core;

public class ObjectTypeConverter : JsonConverter<IObjectType?>
{
  public override bool CanConvert(Type objectType)
  {
    return typeof(IObjectType).IsAssignableFrom(objectType);
  }

  /// <inheritdoc />
  public override void Write(Utf8JsonWriter writer, IObjectType? value, JsonSerializerOptions options)
  {
    var jsonObject = new JsonObject(value?.Properties ?? Enumerable.Empty<KeyValuePair<string, JsonNode?>>());

    jsonObject.WriteTo(writer);
  }

  /// <inheritdoc />
  public override IObjectType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var jsonNode = JsonNode.Parse(ref reader);
    return Activator.CreateInstance(typeToConvert, jsonNode.Deserialize<IReadOnlyDictionary<string, JsonNode?>>()) as IObjectType;
  }
}
