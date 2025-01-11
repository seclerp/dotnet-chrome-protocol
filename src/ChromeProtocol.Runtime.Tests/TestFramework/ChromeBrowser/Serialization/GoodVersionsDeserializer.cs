using System.Text.Json;

namespace ChromeProtocol.Runtime.Tests.TestFramework.ChromeBrowser.Serialization;

public class GoodVersionsDeserializer
{
  public static JsonObjects.GoodVersionsWithDownloads Deserialize(Stream json) =>
    JsonSerializer.Deserialize<JsonObjects.GoodVersionsWithDownloads>(json, JsonObjects.Converter.Settings)
      ?? throw new ArgumentException(null, nameof(json));
}
