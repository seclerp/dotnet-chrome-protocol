using ChromeProtocol.Domains;
using ChromeProtocol.Runtime.Browser;
using ChromeProtocol.Runtime.Messaging.WebSockets;
using ChromeProtocol.Runtime.Tests.TestFramework.ChromeBrowser;
using ChromeProtocol.Tests.Extensions.Logging;
using Xunit.Abstractions;

namespace ChromeProtocol.Runtime.Tests.Launcher;

public class ChromiumLauncherTests
{
  private readonly ITestOutputHelper _outputHelper;
  private readonly IBrowserBinariesStorage _storage;
  private readonly ChromeBrowserDownloader _downloader;

  public ChromiumLauncherTests(ITestOutputHelper outputHelper)
  {
    _outputHelper = outputHelper;
    _storage = new DefaultChromeBinariesStorage(new XunitLogger(outputHelper, "BrowserStorage"));
    _downloader = new ChromeBrowserDownloader(_storage, new XunitLogger(outputHelper, "Downloader"));
  }

  [Fact]
  public async Task ChromiumLauncher_ShouldLaunchSuccessfully()
  {
    await DownloadChromeIfNeeded(ChromeConstants.ChromeVersion);

    var exePath = await _storage.ResolveExePathAsync(ChromeConstants.ChromeVersion);

    using var chrome = await ChromiumLauncher.Create(new XunitLogger(_outputHelper, "Launcher"))
      .WithArguments(
        "--headless",
        "--no-default-browser-check",
        "--no-first-run",
        "--no-sandbox",
        "--disable-extensions",
        "--disable-popup-blocking ",
        "--disable-translate",
        "--disable-background-networking",
        "--disable-sync",
        "--disable-background-timer-throttling",
        "--disable-infobars",
        "--disable-component-extensions-with-background-pages"
      )
      .WithPage("about:blank")
      .LaunchLocalAsync(exePath);

    using var browserClient =
      new DefaultProtocolClient(chrome.DebuggingEndpoint, new XunitLogger(_outputHelper, "BrowserClient"));
    await browserClient.ConnectAsync();
    var targets = await browserClient.SendCommandAsync(Target.GetTargets());
    var pages = targets.TargetInfos.Where(t => t.Type == "page").Select(t => t.Url);

    Assert.Equal(["about:blank"], pages);
  }

  private async Task DownloadChromeIfNeeded(string version)
  {
    if (!await _storage.IsAvailableAsync(version))
      await _downloader.DownloadVersionAsync(version);
  }
}
