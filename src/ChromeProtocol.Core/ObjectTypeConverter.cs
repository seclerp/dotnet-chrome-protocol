using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChromeProtocol.Core;

public class ObjectTypeConverter : JsonConverter
{
  public override bool CanConvert(Type objectType)
  {
    return typeof(IObjectType).IsAssignableFrom(objectType);
  }

  public override void WriteJson(JsonWriter writer, object? instance, JsonSerializer serializer)
  {
    var type = instance?.GetType();
    var properties = type.GetProperty(nameof(IObjectType.Properties)).GetValue(instance) as IDictionary<string, JToken?>;
    var jObject = new JObject(properties);

    jObject.WriteTo(writer);
  }

  public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
  {
    var jObject = JObject.Load(reader);
    var instance = existingValue ?? Activator.CreateInstance(objectType, jObject);

    return instance;
  }
}
