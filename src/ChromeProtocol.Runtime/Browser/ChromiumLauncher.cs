using System.Diagnostics;

namespace ChromeProtocol.Runtime.Browser;

public class ChromiumLauncher
{
  private string? _userProfileDirectory;
  private int? _remoteDebuggingPort;
  private List<string> _customArguments = [];

  private ChromiumLauncher()
  {
  }

  public static ChromiumLauncher Create()
  {
    return new();
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

  public IChromiumBrowser LaunchLocal(string chromiumExePath)
  {
    IEnumerable<string> CollectArguments()
    {
      foreach (var customArgument in _customArguments)
        yield return customArgument;

      if (_userProfileDirectory is {} userProfileDirectory)
        yield return $"--user-data-dir=\"{userProfileDirectory}\"";

      if (_remoteDebuggingPort is {} remoteDebuggingPort)
        yield return $"--remote-debugging-port={remoteDebuggingPort}";
    }

    try
    {
      var arguments = CollectArguments().ToList();

      // Create a new process to launch Chrome.
      ProcessStartInfo processInfo = new ProcessStartInfo
      {
        FileName = chromiumExePath,
        Arguments = string.Join(" ", arguments),
        UseShellExecute = false,
        RedirectStandardOutput = false,
        RedirectStandardError = false,
        CreateNoWindow = true
      };

      // Start the process.
      var process = Process.Start(processInfo);
      if (process != null)
      {
        Console.WriteLine("Chrome launched successfully.");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error launching Chrome: {ex.Message}");
    }
  }
}

public interface IChromiumBrowser : IDisposable
{
  public string UserProfileDirectory { get; }
  public Uri DebuggingEndpoint { get; }
}

internal class LocalChromiumBrowser(string userProfileDirectory, Uri debuggingEndpoint, Process process) : IChromiumBrowser
{
  public string UserProfileDirectory => userProfileDirectory;
  public Uri DebuggingEndpoint => debuggingEndpoint;

  public void Dispose()
  {
    process.Dispose();
  }
}
