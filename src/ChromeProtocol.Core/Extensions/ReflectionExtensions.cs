namespace ChromeProtocol.Core.Extensions;

internal static class ReflectionExtensions
{
  internal static IEnumerable<Type> BaseTypesAndSelf(this Type type)
  {
    while (type != null)
    {
      yield return type;
      type = type.BaseType;
    }
  }
}
