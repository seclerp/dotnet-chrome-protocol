using System.Text;

namespace ChromeProtocol.Tools.CodeGeneration;

public class CsharpTypeInfo
{
  private CsharpTypeInfo(string typeNamespace, string typeName, params CsharpTypeInfo[] genericArguments)
  {
    Namespace = typeNamespace;
    Name = typeName;
    GenericArguments = genericArguments;
    FullName = GetFullName();
  }

  private CsharpTypeInfo(string fullTypeName, params CsharpTypeInfo[] genericArguments)
  {
    var typeName = fullTypeName;
    var typeNamespace = (string)null;

    var lastDotIndex = fullTypeName.LastIndexOf('.');
    if (lastDotIndex != -1)
    {
      typeName = fullTypeName.Substring(lastDotIndex + 1);
      typeNamespace = fullTypeName.Substring(0, lastDotIndex);
    }

    Namespace = typeNamespace;
    Name = typeName;
    GenericArguments = genericArguments;
    FullName = GetFullName();
  }

  private string GetFullName()
  {
    var sb = new StringBuilder();
    if (Namespace is not null)
    {
      sb.Append(Namespace);
      sb.Append('.');
    }

    sb.Append(Name);

    if (GenericArguments.Count > 0)
    {
      var genericArgsList = string.Join(", ", GenericArguments.Select(arg => arg.FullName));
      sb.Append('<');
      sb.Append(genericArgsList);
      sb.Append('>');
    }

    return sb.ToString();
  }

  public string? Namespace { get; } = null;

  public string Name { get; }

  public IReadOnlyCollection<CsharpTypeInfo> GenericArguments { get; }

  public string FullName { get; }

  public static CsharpTypeInfo FromFullyQualifiedName(string fullName)
    => new CsharpTypeInfo(fullName);

  public static CsharpTypeInfo FromTypeName(string typeNamespace, string typeName)
    => new CsharpTypeInfo(typeNamespace, typeName);

  public static CsharpTypeInfo FromGenericType(string typeNamespace, string typeName, params CsharpTypeInfo[] genericArguments)
    => new CsharpTypeInfo(typeNamespace, typeName, genericArguments);

  public static CsharpTypeInfo MakeNullable(CsharpTypeInfo typeInfo)
    => new CsharpTypeInfo($"{typeInfo.FullName}?");
}
