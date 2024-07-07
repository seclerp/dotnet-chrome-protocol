using Newtonsoft.Json.Linq;

namespace ChromeProtocol.Core;

public interface IObjectType
{
  public IReadOnlyDictionary<string, JToken?> Properties { get; }
}
