namespace ChromeProtocol.Runtime.Messaging;

public record ProtocolRequest<TRawParams>(
  int Id,
  string Method,
  TRawParams Params,
  string? SessionId = null
) : IProtocolRequest;
