using System.Text.Json;
using System.Text.Json.Serialization;
using ChromeProtocol.Core.Extensions;

namespace ChromeProtocol.Core;

public class PrimitiveTypeConverter : JsonConverter<object>
{
  static Type? GetValueType(Type objectType) =>
    objectType
      .BaseTypesAndSelf()
      .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(PrimitiveType<>))
      .Select(t => t.GetGenericArguments()[0])
      .FirstOrDefault();

  public override bool CanConvert(Type objectType)
    => GetValueType(objectType) != null;

  /// <inheritdoc />
  public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var valueType = GetValueType(typeToConvert);

    if (reader.TokenType == JsonTokenType.Null || valueType is null)
      return Activator.CreateInstance(typeToConvert, null);

    var value = JsonSerializer.Deserialize(ref reader, valueType, options);
    return Activator.CreateInstance(typeToConvert, value);
  }

  /// <inheritdoc />
  public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
  {
    JsonSerializer.Serialize(writer, ((IPrimitiveType?)value)?.RawValue);
  }
}
