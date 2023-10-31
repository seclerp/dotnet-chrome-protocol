namespace ChromeProtocol.Runtime.Messaging;

public record ProtocolResponse<TRawResult>(
  int Id,
  TRawResult? Result,
  ProtocolErrorInfo? Error,
  string? SessionId = null
) : IProtocolMessage;
