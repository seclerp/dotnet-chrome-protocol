namespace ChromeProtocol.Runtime.Messaging;

public record ProtocolEvent<TParams>(
  string Method,
  TParams Params,
  string? SessionId = null
) : IProtocolEvent;
