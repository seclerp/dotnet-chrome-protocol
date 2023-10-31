namespace ChromeProtocol.Tools.Extensions;

public static partial class PropertyNamingExtensions
{
  public static string ToTitleCase(this string str)
    => $"{char.ToUpperInvariant(str[0])}{str[1..]}";
}
