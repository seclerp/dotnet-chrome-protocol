using System.Text.Json;
using System.Text.Json.Serialization;
using ChromeProtocol.Core;

namespace ChromeProtocol.Runtime.Messaging.Json;

/// <summary>
/// A container for the configured <see cref="JsonSerializerOptions"/> to be used with CDP serialization.
/// </summary>
public static class JsonProtocolSerialization
{
  /// <summary>
  /// A configured instance of <see cref="JsonSerializerOptions"/>.
  /// </summary>
  public static readonly JsonSerializerOptions Settings;

  static JsonProtocolSerialization()
  {
    var interfaces = new[]
    {
      typeof(ICommand),
      typeof(IEvent),
      typeof(IType),
      typeof(IProtocolMessage)
    };

    var assembliesTypes = AppDomain
      .CurrentDomain
      .GetAssemblies()
      .SelectMany(assembly => assembly.GetTypes())
      .Where(implementation => implementation.IsClass && !implementation.IsGenericType);

    var derivedTypes = interfaces.ToDictionary(
      @interface => @interface,
      @interface => assembliesTypes
        .Where(@interface.IsAssignableFrom)
        .ToArray());

    Settings = new JsonSerializerOptions
    {
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      TypeInfoResolver = new PolymorphicTypeResolver(derivedTypes)
    };
  }
}
