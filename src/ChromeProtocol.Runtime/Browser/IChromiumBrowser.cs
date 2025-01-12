using ChromeProtocol.Runtime.Messaging;

namespace ChromeProtocol.Runtime.Browser;

/// <summary>
/// An interface that represents running Chromium browser.
/// </summary>
public interface IChromiumBrowser : IDisposable
{
  /// <summary>
  /// A debugging endpoint that could be used to initialize CDP connection using <see cref="IProtocolClient"/>.
  /// </summary>
  public Uri DebuggingEndpoint { get; }
}
