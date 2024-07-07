using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChromeProtocol.Core;

public class ArrayTypeConverter : JsonConverter
{
  public override bool CanConvert(Type objectType)
  {
    return typeof(IArrayType).IsAssignableFrom(objectType);
  }

  public override void WriteJson(JsonWriter writer, object? instance, JsonSerializer serializer)
  {
    var type = instance?.GetType();
    var properties = type.GetProperty(nameof(IArrayType.Items)).GetValue(instance) as IReadOnlyCollection<JToken>;
    var jArray = new JArray(properties);

    jArray.WriteTo(writer);
  }

  public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
  {
    var jArray = JArray.Load(reader);
    var instance = existingValue ?? Activator.CreateInstance(objectType, jArray.ToObject<IReadOnlyCollection<JToken>>());

    return instance;
  }
}
