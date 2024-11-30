using System.Diagnostics;

namespace ChromeProtocol.Runtime.Browser;

internal class LocalChromiumBrowser(Uri debuggingEndpoint, Process process) : IChromiumBrowser
{
  public Uri DebuggingEndpoint => debuggingEndpoint;
  public Process Process => process;

  public void Dispose()
  {
    process.Dispose();
  }
}
