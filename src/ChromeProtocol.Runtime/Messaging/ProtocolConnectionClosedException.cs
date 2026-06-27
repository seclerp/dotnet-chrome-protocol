namespace ChromeProtocol.Runtime.Messaging;

/// <summary>
/// Represents a protocol command failure caused by the underlying connection closing before a response is received.
/// </summary>
public class ProtocolConnectionClosedException : Exception
{
  /// <summary>
  /// Initializes a new instance of the <see cref="ProtocolConnectionClosedException"/> class.
  /// </summary>
  public ProtocolConnectionClosedException()
    : this("The protocol connection was closed before a response was received.")
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ProtocolConnectionClosedException"/> class with a specified error message.
  /// </summary>
  /// <param name="message">The message that describes the error.</param>
  public ProtocolConnectionClosedException(string message)
    : base(message)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ProtocolConnectionClosedException"/> class with a specified error message and inner exception.
  /// </summary>
  /// <param name="message">The message that describes the error.</param>
  /// <param name="innerException">The exception that is the cause of the current exception.</param>
  public ProtocolConnectionClosedException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
