namespace ChromeProtocol.Core;

/// <summary>
/// Specifies method name to be used in context of given domain command or event.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class MethodNameAttribute : Attribute
{
  /// <summary>
  /// A name of the domain method to be used with the command or event.
  /// </summary>
  public string MethodName { get; }

  /// <summary>
  /// Creates instance of type <see cref="MethodNameAttribute"/>.
  /// </summary>
  /// <param name="methodName">A name of the domain method.</param>
  public MethodNameAttribute(string methodName)
  {
    MethodName = methodName;
  }
}
