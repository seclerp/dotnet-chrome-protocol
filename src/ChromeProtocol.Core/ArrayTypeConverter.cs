using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace ChromeProtocol.Core;

public class ArrayTypeConverter : JsonConverter<IArrayType?>
{
  public override bool CanConvert(Type objectType)
  {
    return typeof(IArrayType).IsAssignableFrom(objectType);
  }

  /// <inheritdoc />
  public override void Write(Utf8JsonWriter writer, IArrayType? value, JsonSerializerOptions options)
  {
    var jsonArray = new JsonArray(value?.Items.ToArray() ?? Array.Empty<JsonNode>());

    jsonArray.WriteTo(writer);
  }

  /// <inheritdoc />
  public override IArrayType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var jsonNode = JsonNode.Parse(ref reader);
    return Activator.CreateInstance(typeToConvert, jsonNode.Deserialize<IReadOnlyCollection<JsonNode>>()) as IArrayType;
  }
}
