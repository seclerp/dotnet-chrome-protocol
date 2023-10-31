using ChromeProtocol.Core.Extensions;
using Newtonsoft.Json;

namespace ChromeProtocol.Core;

public class PrimitiveTypeConverter : JsonConverter
{
  static Type? GetValueType(Type objectType) =>
    objectType
      .BaseTypesAndSelf()
      .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(PrimitiveType<>))
      .Select(t => t.GetGenericArguments()[0])
      .FirstOrDefault();

  public override bool CanConvert(Type objectType) => GetValueType(objectType) != null;

  public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
  {
    if (reader.TokenType == JsonToken.Null)
      return Activator.CreateInstance(objectType, null);

    var valueType = GetValueType(objectType);
    var value = serializer.Deserialize(reader, valueType);

    return Activator.CreateInstance(objectType, value);
  }

  public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
  {
    serializer.Serialize(writer, ((IPrimitiveType?)value)?.RawValue);
  }
}
