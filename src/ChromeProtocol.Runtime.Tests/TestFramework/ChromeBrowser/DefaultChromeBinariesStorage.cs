using System.IO.Compression;
using System.Runtime.InteropServices;

namespace ChromeProtocol.Runtime.Tests.TestFramework.ChromeBrowser;

public class DefaultChromeBinariesStorage : IBrowserBinariesStorage
{
  private readonly string StoragePath = Path.Combine(Path.GetTempPath(), "ChromiumProtocol.Tests.ChromeBinaries");

  public Task<bool> IsAvailableAsync(string version) =>
    Task.FromResult(
      Directory.Exists(GetFullDestinationPath(version))
      && TryResolveExePath(version, out var path)
      && File.Exists(path)
    );

  public async Task SaveFromArchiveAsync(string version, Stream archive, bool replaceExisting = false)
  {
    if (!archive.CanSeek)
      throw new ArgumentException("A seekable archive is expected.", nameof(archive));

    var destinationPath = GetFullDestinationPath(version);

    if (await IsAvailableAsync(version) && !replaceExisting)
      throw new Exception($"Version '{version}' is already saved to storage at '{destinationPath}'.");

    var position = archive.Position;
    archive.Seek(0, SeekOrigin.Begin);

    if (replaceExisting && Directory.Exists(destinationPath))
      Directory.Delete(destinationPath, true);

    Directory.CreateDirectory(destinationPath);

    ExtractZipToDirectory(archive, destinationPath);
    archive.Seek(position, SeekOrigin.Begin);
  }

  public Task<string> ResolveExePathAsync(string version)
  {
    if (!TryResolveExePath(version, out var path))
    {
      throw new ArgumentException($"Can't resolve Chrome executable path on the currently running platform: {RuntimeInformation.OSDescription}");
    }

    if (!File.Exists(path))
      throw new FileNotFoundException($"Chrome executable is expected to be at '{path}' but it was not found.");

    return Task.FromResult(path);
  }

  private bool TryResolveExePath(string version, out string? path)
  {
    var platformFolder = RuntimeInformation.OSArchitecture switch
    {
      Architecture.X64 when RuntimeInformation.IsOSPlatform(OSPlatform.Linux) => "chrome-linux64",
      Architecture.X64 when RuntimeInformation.IsOSPlatform(OSPlatform.Windows) => "chrome-win64",
      Architecture.X64 when RuntimeInformation.IsOSPlatform(OSPlatform.OSX) => "chrome-mac-x64",
      Architecture.Arm64 when RuntimeInformation.IsOSPlatform(OSPlatform.OSX) => "chrome-mac-arm64",
      _ => throw new ArgumentOutOfRangeException(
        $"The architecture is not supported: {RuntimeInformation.OSArchitecture}, OS: {RuntimeInformation.OSDescription}")
    };

    path = null;

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
      path = Path.Combine(StoragePath, version, platformFolder, "chrome.exe");
      return true;
    }

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
      path = Path.Combine(StoragePath, version, platformFolder, "chrome");
      return true;
    }

    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
      path = Path.Combine(StoragePath, version, platformFolder, "Google Chrome for Testing.app", "Contents", "MacOS", "Google Chrome for Testing");
      return true;
    }

    return false;
  }

  private void ExtractZipToDirectory(Stream zipStream, string destinationDirectory)
  {
    using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read, leaveOpen: true);
    archive.ExtractToDirectory(Path.Combine(destinationDirectory), true);
  }

  private string GetFullDestinationPath(string version)
  {
    return Path.Combine(StoragePath, version);
  }
}
