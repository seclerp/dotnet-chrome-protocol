namespace ChromeProtocol.Runtime.Tests.TestFramework.ChromeBrowser;

public interface IBrowserBinariesStorage
{
  Task<bool> IsAvailableAsync(string version);
  Task SaveFromArchiveAsync(string version, Stream archive, bool replaceExisting);
  Task<string> ResolveExePathAsync(string version);
}
