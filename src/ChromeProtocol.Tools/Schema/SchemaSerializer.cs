using System.Text.Json;
using System.Text.Json.Serialization;
using ChromeProtocol.Tools.Schema.Models;

namespace ChromeProtocol.Tools.Schema;

public static class SchemaSerializer
{
  private static JsonSerializerOptions _serializerOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
  };

  public static Definition ParseDefinition(Stream definitionStream)
  {
    return JsonSerializer.Deserialize<Definition>(definitionStream, _serializerOptions)!;
  }

  public static Definition ParseDefinition(JsonDocument definitionJson)
  {
    return definitionJson.Deserialize<Definition>(_serializerOptions)!;
  }
}
