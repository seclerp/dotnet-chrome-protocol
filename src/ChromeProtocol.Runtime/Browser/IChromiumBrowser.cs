namespace ChromeProtocol.Runtime.Browser;

public interface IChromiumBrowser : IDisposable
{
  public Uri DebuggingEndpoint { get; }
}
