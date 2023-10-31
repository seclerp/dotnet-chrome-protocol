using ChromeProtocol.Tools.CodeGeneration.Models;
using ChromeProtocol.Tools.Extensions;

namespace ChromeProtocol.Tools.CodeGeneration.Services;

public static class CsharpNameResolver
{
  public static string Resolve(string itemName, ItemKind itemKind, string? enclosingTypeName = null)
  {
    string ActualName() =>
      itemKind switch
      {
        ItemKind.TypeName => $"{itemName}Type",
        ItemKind.EventName => itemName.ToTitleCase(),
        ItemKind.CommandName => $"{itemName.ToTitleCase()}Request",
        ItemKind.PropertyName => itemName.ToTitleCase(),
        _ => throw new ArgumentOutOfRangeException(nameof(itemKind), itemKind, null)
      };

    string NewName() =>
      itemKind switch
      {
        ItemKind.PropertyName => $"{itemName.ToTitleCase()}Property",
        _ => ActualName()
      };

    var actualName = ActualName();
    if (actualName.Equals(enclosingTypeName))
      return NewName();

    return actualName;
  }
}
