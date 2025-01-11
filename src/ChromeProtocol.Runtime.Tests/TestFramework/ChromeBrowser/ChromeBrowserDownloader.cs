using System.Runtime.InteropServices;
using ChromeProtocol.Runtime.Tests.TestFramework.ChromeBrowser.Serialization;

namespace ChromeProtocol.Runtime.Tests.TestFramework.ChromeBrowser;

public class ChromeBrowserDownloader(IBrowserBinariesStorage browserBinariesStorage, ILogger logger)
{
  private const string GoodVersionsWithDownloadsEndpoint = "https://googlechromelabs.github.io/chrome-for-testing/known-good-versions-with-downloads.json";

  public async Task DownloadVersionAsync(string version)
  {
    using var httpClient = new HttpClient();

    logger.LogInformation("Fetching the 'known-good-versions-with-downloads.json'...");
    using var versionsResponse = await httpClient.GetAsync(GoodVersionsWithDownloadsEndpoint).ConfigureAwait(false);
    versionsResponse.EnsureSuccessStatusCode();
    await using var responseStream = await versionsResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);
    logger.LogInformation("OK");

    logger.LogInformation("Parsing 'known-good-versions-with-downloads.json'...");
    var goodVersions = GoodVersionsDeserializer.Deserialize(responseStream);
    logger.LogInformation("OK");

    var platform = GetJsonPlatform();
    logger.LogInformation($"Detected Chrome download platform: {platform}");

    var chromeDownloadsPerVersion =
      goodVersions.Versions.ToDictionary(v => v.VersionVersion,
        v => v.Downloads.Chrome);

    if (!chromeDownloadsPerVersion.TryGetValue(version, out var platformDownloads))
    {
      throw new Exception($"There is no downloads for version {version}");
    }

    logger.LogInformation($"Found downloads for version '{version}'");

    var platformDownloadLink = platformDownloads.FirstOrDefault(download => download.Platform == platform)?.Url;
    if (platformDownloadLink is null)
    {
      throw new Exception($"There is no downloads for version '{version}' and platform '{platform}'");
    }

    logger.LogInformation($"Found download link for the platform '{platform}': '{platformDownloadLink}'");

    var downloadDestinationFile = Path.GetTempFileName();

    logger.LogInformation($"Downloading to the temporary file: '{platform}': '{platformDownloadLink}'");

    await DownloadFileWithProgressAsync(platformDownloadLink, downloadDestinationFile);
    await using var fileStream = new FileStream(downloadDestinationFile, FileMode.OpenOrCreate, FileAccess.Read);

    logger.LogInformation($"Downloading is done, unpacking and saving to the storage...");

    await browserBinariesStorage.SaveFromArchiveAsync(version, fileStream, replaceExisting: true);

    logger.LogInformation("Storing is done");
  }

  private async Task DownloadFileWithProgressAsync(Uri url, string destinationPath)
  {
    using var httpClient = new HttpClient();
    using var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

    response.EnsureSuccessStatusCode();

    var totalBytes = response.Content.Headers.ContentLength;
    var bufferSize = 8192;

    await using var contentStream = await response.Content.ReadAsStreamAsync();
    await using var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true);

    var buffer = new byte[bufferSize];
    long totalBytesRead = 0;
    int bytesRead;
    var lastProgress = 0;

    while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
    {
      await fileStream.WriteAsync(buffer, 0, bytesRead);
      totalBytesRead += bytesRead;

      if (totalBytes.HasValue)
      {
        var progress = (int)(totalBytesRead * 100L / totalBytes.Value);
        if (progress != lastProgress)
        {
          lastProgress = progress;
          logger.LogInformation($"Progress: {progress}%");
        }
      }
      else
      {
        logger.LogInformation($"Downloaded {totalBytesRead} bytes...");
      }
    }
  }

  private JsonObjects.Platform GetJsonPlatform() =>
    RuntimeInformation.OSArchitecture switch
    {
      Architecture.X64 when RuntimeInformation.IsOSPlatform(OSPlatform.Linux) => JsonObjects.Platform.Linux64,
      Architecture.X64 when RuntimeInformation.IsOSPlatform(OSPlatform.Windows) => JsonObjects.Platform.Win64,
      Architecture.X64 when RuntimeInformation.IsOSPlatform(OSPlatform.OSX) => JsonObjects.Platform.MacX64,
      Architecture.Arm64 when RuntimeInformation.IsOSPlatform(OSPlatform.OSX) => JsonObjects.Platform.MacArm64,
      _ => throw new ArgumentOutOfRangeException(
        $"The architecture is not supported: {RuntimeInformation.OSArchitecture}, OS: {RuntimeInformation.OSDescription}")
    };
}
