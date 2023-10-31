using Newtonsoft.Json.Linq;

namespace ChromeProtocol.Runtime.Messaging;

public record ProtocolErrorInfo(
  int Code,
  JObject? Result,
  string? Message,
  string? Data
);
