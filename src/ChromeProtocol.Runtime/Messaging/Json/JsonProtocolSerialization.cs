using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ChromeProtocol.Runtime.Messaging.Json;

public static class JsonProtocolSerialization
{
  public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
  {
    NullValueHandling = NullValueHandling.Ignore,
    ContractResolver = new CamelCasePropertyNamesContractResolver()
  };
}
