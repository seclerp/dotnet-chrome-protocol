using ChromeProtocol.Domains;
using ChromeProtocol.Runtime.Browser;
using ChromeProtocol.Runtime.Messaging.Logging;
using ChromeProtocol.Runtime.Messaging.WebSockets;
using ChromeProtocol.Usage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Console = System.Console;

const string PageUrl = "https://google.com";

using var chromium = await ChromiumLauncher.Create()
  .WithUserProfileDirectory("D:\\TestCookies")//Directory.CreateTempSubdirectory("ChromiumUserData").FullName)
  .WithRemoteDebuggingPort(0)
  .WithArguments(
    "--no-default-browser-check",
    "--no-first-run",
    "--disable-extensions",
    "--disable-popup-blocking ",
    "--disable-translate",
    "--disable-background-networking",
    "--disable-sync",
    "--disable-background-timer-throttling",
    "--disable-infobars",
    "--disable-component-extensions-with-background-pages"
  )
  .WithPage(PageUrl)
  .LaunchLocalAsync(@"C:\Program Files\Google\Chrome\Application\chrome.exe");

using var browserConnection = new DefaultProtocolClient(chromium.DebuggingEndpoint, NullLogger.Instance);
var clientLogger =
  new DefaultProtocolClientLogger(browserConnection, new SimpleConsoleLogger(nameof(Program), LogLevel.Trace));
clientLogger.StartLogging();

await browserConnection.ConnectAsync();
var pages = await browserConnection.SendCommandAsync(Target.GetTargets());
var page = pages.TargetInfos.FirstOrDefault(target => target.Type == "page");

var attachResult = await browserConnection.SendCommandAsync(Target.AttachToTarget(page.TargetId, Flatten: true));
using var pageConnection = browserConnection.CreateScoped(attachResult.SessionId.Value);

var result = await pageConnection.SendCommandAsync(Network.GetCookies([PageUrl]));

Console.WriteLine(chromium.DebuggingEndpoint);


