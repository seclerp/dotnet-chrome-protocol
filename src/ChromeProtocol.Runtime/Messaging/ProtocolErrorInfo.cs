using System.Text.Json.Nodes;

namespace ChromeProtocol.Runtime.Messaging;

public record ProtocolErrorInfo(
  int Code,
  JsonObject? Result,
  string? Message,
  string? Data
);
