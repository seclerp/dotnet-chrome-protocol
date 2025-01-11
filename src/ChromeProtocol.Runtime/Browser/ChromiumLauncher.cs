using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ChromeProtocol.Runtime.Browser;

public class ChromiumLauncher
{
  private string? _pageUrl;
  private string? _userProfileDirectory;
  private int? _remoteDebuggingPort;
  private List<string> _customArguments = [];

  private readonly int _portReadingRetries = 10;
  private readonly int _portReadingDelay = 500;

  private readonly ILogger _logger;

  private ChromiumLauncher(ILogger logger)
  {
    _logger = logger;
  }

  public static ChromiumLauncher Create(ILogger logger)
  {
    return new(logger);
  }

  public static ChromiumLauncher Create()
  {
    return new(NullLogger.Instance);
  }

  public ChromiumLauncher WithPage(string pageUrl)
  {
    _pageUrl = pageUrl;

    return this;
  }

  public ChromiumLauncher WithUserProfileDirectory(string path)
  {
    _userProfileDirectory = path;

    return this;
  }

  public ChromiumLauncher WithRemoteDebuggingPort(int port)
  {
    _remoteDebuggingPort = port;

    return this;
  }

  public ChromiumLauncher WithArgument(string arg)
  {
    _customArguments.Add(arg);

    return this;
  }

  public ChromiumLauncher WithArguments(params string[] args)
  {
    _customArguments.AddRange(args);

    return this;
  }

  /// <summary>
  /// For Windows and Linux it should be a full path to the executable
  /// For MacOS it should be the full path to the native executable inside package.
  /// </summary>
  /// <param name="chromiumExePath"></param>
  /// <returns></returns>
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

    return new LocalChromiumBrowser(debuggingEndpoint, process);
  }

  private async Task<Uri> WaitDebuggingEndpointAsync(Process process)
  {
    var outputBuilder = new StringBuilder();
    while (!process.StandardError.EndOfStream)
    {
      var line = await process.StandardError.ReadLineAsync().ConfigureAwait(false);
      outputBuilder.Append(line);
      if (!string.IsNullOrEmpty(line))
      {
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
