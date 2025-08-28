using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ChromeProtocol.Runtime.Browser;

/// <summary>
/// A Chromium browser launcher.
/// </summary>
public class ChromiumLauncher
{
  private string? _pageUrl;
  private string? _userProfileDirectory;
  private int? _remoteDebuggingPort;
  private List<string> _customArguments = [];

  private readonly ILogger _logger;

  private ChromiumLauncher(ILogger logger)
  {
    _logger = logger;
  }

  /// <summary>
  /// Create <see cref="ChromiumLauncher"/> instance with the given logger.
  /// </summary>
  /// <param name="logger">A logger instance.</param>
  /// <returns>A new <see cref="ChromiumLauncher"/> instance.</returns>
  public static ChromiumLauncher Create(ILogger logger) => new(logger);

  /// <summary>
  /// Create <see cref="ChromiumLauncher"/> instance.
  /// </summary>
  /// <returns>A new <see cref="ChromiumLauncher"/> instance.</returns>
  public static ChromiumLauncher Create() => new(NullLogger.Instance);

  /// <summary>
  /// Sets the page that will be opened in the browser tab.
  /// </summary>
  /// <param name="pageUrl"></param>
  /// <returns>An updated <see cref="ChromiumLauncher"/> instance.</returns>
  public ChromiumLauncher WithPage(string pageUrl)
  {
    _pageUrl = pageUrl;

    return this;
  }

  /// <summary>
  /// Sets the user's profile directory that will be used by the browser.
  /// </summary>
  /// <param name="path">A path to the directory.</param>
  /// <returns>An updated <see cref="ChromiumLauncher"/> instance.</returns>
  /// <remarks>Transforms into --user-data-dir="value" command line argument.</remarks>
  public ChromiumLauncher WithUserProfileDirectory(string path)
  {
    _userProfileDirectory = path;

    return this;
  }

  /// <summary>
  /// Sets the TCP remote debugging port that will be exposed by the browser.<br/>
  /// If set to 0, browser will choose and allocate port number automatically.
  /// </summary>
  /// <param name="port">A port number.</param>
  /// <returns>An updated <see cref="ChromiumLauncher"/> instance.</returns>
  /// <remarks>Transforms into --remote-debugging-port=value command line argument.</remarks>
  public ChromiumLauncher WithRemoteDebuggingPort(int port)
  {
    _remoteDebuggingPort = port;

    return this;
  }

  /// <summary>
  /// Adds custom command line argument to the arguments' list.
  /// </summary>
  /// <param name="arg">An argument value.</param>
  /// <returns>An updated <see cref="ChromiumLauncher"/> instance.</returns>
  /// <remarks>Argument will be added after special pretending --args argument.</remarks>
  public ChromiumLauncher WithArgument(string arg)
  {
    _customArguments.Add(arg);

    return this;
  }

  /// <summary>
  /// Adds custom command line arguments to the arguments' list.
  /// </summary>
  /// <param name="args">An argument values collection.</param>
  /// <returns>An updated <see cref="ChromiumLauncher"/> instance.</returns>
  /// <remarks>Argument will be added after special pretending --args argument.</remarks>
  public ChromiumLauncher WithArguments(params string[] args)
  {
    _customArguments.AddRange(args);

    return this;
  }

  /// <summary>
  /// Launches the Chromium browser with the given path,
  /// using the options configured by this <see cref="ChromiumLauncher"/> instance.
  /// </summary>
  /// <param name="chromiumExePath">A path to the Chromium executable.</param>
  /// <returns></returns>
  /// <remarks>For Windows and Linux, a path should be a full path to the executable.</remarks>
  /// <remarks>For MacOS, a path should be the full path to the native executable inside package.</remarks>
  /// <example>LaunchLocalAsync("C:\Program Files\Google\Chrome\Application\chrome.exe")</example>
  /// <example>LaunchLocalAsync("/Applications/Google Chrome.app/Contents/MacOS/Google Chrome")</example>
  public async Task<IChromiumBrowser> LaunchLocalAsync(string chromiumExePath)
  {
    IEnumerable<string> CollectArguments()
    {
      if (_pageUrl is not null)
        yield return _pageUrl;

      foreach (var customArgument in _customArguments)
        yield return customArgument;

      if (_userProfileDirectory is {} userProfileDirectory)
        yield return $"--user-data-dir=\"{userProfileDirectory}\"";

      var remoteDebuggingPort = _remoteDebuggingPort ?? 0;
      yield return $"--remote-debugging-port={remoteDebuggingPort}";
    }

    var arguments = CollectArguments().ToList();

    // Create a new process to launch Chrome.
    var processInfo = new ProcessStartInfo
    {
      FileName = chromiumExePath,
      Arguments = string.Join(" ", arguments),
      UseShellExecute = false,
      RedirectStandardOutput = false,
      RedirectStandardError = true,
      CreateNoWindow = false
    };

    // Start the process.
    var process = Process.Start(processInfo);
    var debuggingEndpoint = await WaitDebuggingEndpointAsync(process).ConfigureAwait(false);

    return new LocalChromiumBrowser(debuggingEndpoint, process, _logger);
  }

  private async Task<Uri> WaitDebuggingEndpointAsync(Process process)
  {
    var outputBuilder = new StringBuilder();
    while (!process.StandardError.EndOfStream)
    {
      var line = await process.StandardError.ReadLineAsync().ConfigureAwait(false);
      if (!string.IsNullOrEmpty(line))
      {
        outputBuilder.Append(line);
        // Sample expected line format: "DevTools listening on ws://127.0.0.1:PORT/PATH"
        var match = Regex.Match(line, @"ws:\/\/127\.0\.0\.1:(\d+)(\/.+)?");
        if (match.Success)
        {
          var port = match.Groups[1].Value;
          var path = match.Groups[2].Value;

          return new Uri($"ws://127.0.0.1:{port}{path}");
        }
      }
    }

    throw new Exception($"Expected debugging endpoint in the stderr. Got:\n{outputBuilder}");
  }
}
