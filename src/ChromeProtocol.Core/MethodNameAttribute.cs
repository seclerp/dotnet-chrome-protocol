namespace ChromeProtocol.Core;

/// <summary>
/// Specifies method name to be used in context of given command or event.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class MethodNameAttribute : Attribute
{
  public string MethodName { get; }

  public MethodNameAttribute(string methodName)
  {
    MethodName = methodName;
  }
}
