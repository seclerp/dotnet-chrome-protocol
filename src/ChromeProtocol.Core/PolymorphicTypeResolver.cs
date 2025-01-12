using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace ChromeProtocol.Core;

/// <summary>
/// A JSON type resolver that uses pre-computed CDP type derivatives to handle cases when System.Text.Json
/// can't resolve proper inherited type.
/// </summary>
public sealed class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
{
  private readonly Dictionary<Type, Type[]> _derivedTypes;

  /// <inheritdoc />
  public PolymorphicTypeResolver(Dictionary<Type, Type[]> derivedTypes)
    => _derivedTypes = derivedTypes;

  public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
  {
    var typeInfo = base.GetTypeInfo(type, options);

    if (!_derivedTypes.TryGetValue(typeInfo.Type, out var implementations))
      return typeInfo;

    if (implementations.Length == 0)
      return typeInfo;

    var polymorphismOptions = new JsonPolymorphismOptions
    {
      // TypeDiscriminatorPropertyName = "$type",
      IgnoreUnrecognizedTypeDiscriminators = true,
      UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
    };

    foreach (var derivedType in implementations.Select(implementation => new JsonDerivedType(implementation)))
    {
      polymorphismOptions.DerivedTypes.Add(derivedType);
    }

    typeInfo.PolymorphismOptions = polymorphismOptions;

    return typeInfo;
  }
}
