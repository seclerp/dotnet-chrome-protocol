using Newtonsoft.Json.Linq;

namespace ChromeProtocol.Core;

public interface IArrayType
{
  public IReadOnlyCollection<JToken?> Items { get; }
}
