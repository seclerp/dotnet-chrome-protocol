namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public static class BuilderDslExtensions
{
  public static T Apply<T>(this T instance, Action<T> action)
  {
    action.Invoke(instance);
    return instance;
  }

  public static T Apply<T>(this T instance, Func<T, T> action)
  {
    return action.Invoke(instance);
  }

  public static T ApplyIf<T>(this T instance, bool condition, Action<T> action)
  {
    if (condition)
    {
      return instance.Apply(action);
    }
    return instance;
  }

  public static T ApplyIf<T>(this T instance, bool condition, Func<T, T> action)
  {
    if (condition)
    {
      return instance.Apply(action);
    }
    return instance;
  }
}
