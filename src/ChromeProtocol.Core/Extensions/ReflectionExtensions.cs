namespace ChromeProtocol.Core.Extensions;

public static class ReflectionExtensions
{
  public static IEnumerable<Type> BaseTypesAndSelf(this Type type)
  {
    while (type != null)
    {
      yield return type;
      type = type.BaseType;
    }
  }
}
