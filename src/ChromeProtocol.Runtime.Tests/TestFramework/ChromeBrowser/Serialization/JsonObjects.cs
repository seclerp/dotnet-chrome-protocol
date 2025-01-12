using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChromeProtocol.Runtime.Tests.TestFramework.ChromeBrowser.Serialization;

public class JsonObjects
{
  public class GoodVersionsWithDownloads
  {
    [JsonPropertyName("timestamp")] public required DateTimeOffset Timestamp { get; set; }

    [JsonPropertyName("versions")] public required Version[] Versions { get; set; }
  }

  public class Version
  {
    [JsonPropertyName("version")] public required string VersionVersion { get; set; }

    [JsonPropertyName("revision")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Revision { get; set; }

    [JsonPropertyName("downloads")] public required Downloads Downloads { get; set; }
  }

  public class Downloads
  {
    [JsonPropertyName("chrome")] public required Chrome[] Chrome { get; set; }

    // TODO: Reconsider whether this should be completely removed or not.
    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    // [JsonPropertyName("chromedriver")]
    // public required Chrome[] Chromedriver { get; set; }
    //
    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    // [JsonPropertyName("chrome-headless-shell")]
    // public required Chrome[] ChromeHeadlessShell { get; set; }
  }

  public class Chrome
  {
    [JsonPropertyName("platform")] public Platform Platform { get; set; }

    [JsonPropertyName("url")] public required Uri Url { get; set; }
  }

  public enum Platform
  {
    Linux64,
    MacArm64,
    MacX64,
    Win32,
    Win64
  };

  internal static class Converter
  {
    public static readonly JsonSerializerOptions Settings = new(JsonSerializerDefaults.General)
    {
      Converters =
      {
        PlatformConverter.Singleton,
        new DateOnlyConverter(),
        new TimeOnlyConverter(),
        IsoDateTimeOffsetConverter.Singleton
      },
    };
  }

  internal class PlatformConverter : JsonConverter<Platform>
  {
    public override bool CanConvert(Type t) => t == typeof(Platform);

    public override Platform Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      switch (value)
      {
        case "linux64":
          return Platform.Linux64;
        case "mac-arm64":
          return Platform.MacArm64;
        case "mac-x64":
          return Platform.MacX64;
        case "win32":
          return Platform.Win32;
        case "win64":
          return Platform.Win64;
      }

      throw new Exception("Cannot unmarshal type Platform");
    }

    public override void Write(Utf8JsonWriter writer, Platform value, JsonSerializerOptions options)
    {
      switch (value)
      {
        case Platform.Linux64:
          JsonSerializer.Serialize(writer, "linux64", options);
          return;
        case Platform.MacArm64:
          JsonSerializer.Serialize(writer, "mac-arm64", options);
          return;
        case Platform.MacX64:
          JsonSerializer.Serialize(writer, "mac-x64", options);
          return;
        case Platform.Win32:
          JsonSerializer.Serialize(writer, "win32", options);
          return;
        case Platform.Win64:
          JsonSerializer.Serialize(writer, "win64", options);
          return;
      }

      throw new Exception("Cannot marshal type Platform");
    }

    public static readonly PlatformConverter Singleton = new PlatformConverter();
  }

  internal class ParseStringConverter : JsonConverter<long>
  {
    public override bool CanConvert(Type t) => t == typeof(long);

    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var value = reader.GetString();
      long l;
      if (Int64.TryParse(value, out l))
      {
        return l;
      }

      throw new Exception("Cannot unmarshal type long");
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
      JsonSerializer.Serialize(writer, value.ToString(), options);
      return;
    }

    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
  }
}
