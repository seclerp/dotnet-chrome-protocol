using ChromeProtocol.Tools.CodeGeneration.Models;
using ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;
using ChromeProtocol.Tools.Schema.Models;

namespace ChromeProtocol.Tools.CodeGeneration.Services;

public static class CsharpTypeResolver
{
  public static CsharpTypeInfo Resolve(string @namespace, string domainName, string? typeRef, TypeKind? kind, ValidatedItems? items, bool isOptional)
  {
    var coreType =
      (kind, typeRef) switch
      {
        (TypeKind.String, _) => CsharpTypeInfo.FromFullyQualifiedName("string"),
        (TypeKind.Number, _) => CsharpTypeInfo.FromFullyQualifiedName("double"),
        (TypeKind.Integer, _) => CsharpTypeInfo.FromFullyQualifiedName("int"),
        (TypeKind.Boolean, _) => CsharpTypeInfo.FromFullyQualifiedName("bool"),
        (TypeKind.Array, _) when items is null => throw new Exception(
          "Properties of type 'array' without 'items' specified are not supported."),
        (TypeKind.Array, _) => CsharpTypeInfo.FromGenericType("System.Collections.Generic", "IReadOnlyList",
          Resolve(@namespace, domainName, items.Ref, items.Type, null, false)),
        (TypeKind.Object, null or "") => CsharpTypeInfo.FromTypeName("Newtonsoft.Json.Linq", "JObject"),
        (_, not null and not "") => CsharpTypeInfo.FromTypeName(@namespace,
          CsharpNameResolver.Resolve(typeRef.Contains('.') ? typeRef : $"{domainName}.{typeRef}", ItemKind.TypeName, domainName)),
        (TypeKind.Any, null or "") => CsharpTypeInfo.FromTypeName("Newtonsoft.Json.Linq", "JToken"),
        _ => throw new ArgumentOutOfRangeException($"({nameof(kind)}, {nameof(typeRef)})")
      };

    return isOptional ? CsharpTypeInfo.MakeNullable(coreType) : coreType;
  }
}
