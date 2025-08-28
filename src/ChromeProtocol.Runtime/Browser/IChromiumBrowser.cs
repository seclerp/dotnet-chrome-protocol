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
  Uri DebuggingEndpoint { get; }

  /// <summary>
  /// Creates a new <see cref="DefaultProtocolClient"/> connected to <see cref="DebuggingEndpoint"/> and returns it as <see cref="IProtocolClient"/>.
  /// </summary>
  Task<IProtocolClient> AttachAsync(CancellationToken token = default);

  /// <summary>
  /// Dynamic collection of currently available Chromium targets (polled from /json endpoint).
  /// </summary>
  ObservableCollection<ChromiumTarget> Targets { get; }

  /// <summary>
  /// Shortcut to targets filtered by type == "page".
  /// </summary>
  ObservableCollection<ChromiumTarget> Pages { get; }

  /// <summary>
  /// Attaches to a specific target and returns a scoped protocol client bound to that target session.
  /// </summary>
  Task<IScopedProtocolClient> AttachToTargetAsync(ChromiumTarget target, CancellationToken token = default);

  /// <summary>
  /// Attaches to a specific target by its id and returns a scoped protocol client bound to that target session.
  /// </summary>
  Task<IScopedProtocolClient> AttachToTargetAsync(string targetId, CancellationToken token = default);
}
