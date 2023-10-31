namespace ChromeProtocol.Runtime.Messaging;

public class ProtocolErrorException : Exception
{
  public ProtocolErrorInfo Info { get; }

  public ProtocolErrorException(ProtocolErrorInfo info)
    : base($"Protocol request failed with code '{info.Code}' and error '{info.Message}'" +
           $", see {nameof(Info)} property for details.")
  {
    Info = info;
  }
}
