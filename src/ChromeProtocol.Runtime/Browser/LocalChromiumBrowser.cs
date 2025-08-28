using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using ChromeProtocol.Domains;
using ChromeProtocol.Runtime.Messaging;
using ChromeProtocol.Runtime.Messaging.WebSockets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ChromeProtocol.Runtime.Browser;

internal class LocalChromiumBrowser : IChromiumBrowser
{
  private readonly Process _process;
  private readonly ILogger _logger;
  private readonly CancellationTokenSource _cts = new();
  private readonly HttpClient _httpClient = new();
  private readonly Task _pollingTask;

  public LocalChromiumBrowser(Uri debuggingEndpoint, Process process, ILogger? logger = null)
  {
    DebuggingEndpoint = debuggingEndpoint;
    _process = process;
    _logger = logger ?? NullLogger.Instance;

    Targets = new ObservableCollection<ChromiumTarget>();
    Pages = new ObservableCollection<ChromiumTarget>();

    _pollingTask = Task.Run(() => PollTargetsLoopAsync(_cts.Token));
  }

  public Uri DebuggingEndpoint { get; }
  public Process Process => _process;

  public ObservableCollection<ChromiumTarget> Targets { get; }

  public ObservableCollection<ChromiumTarget> Pages { get; }

  public async Task<IProtocolClient> AttachAsync(CancellationToken token = default)
  {
    var client = new DefaultProtocolClient(DebuggingEndpoint, _logger);
    await client.ConnectAsync(token).ConfigureAwait(false);
    return client;
  }

  public Task<IScopedProtocolClient> AttachToTargetAsync(ChromiumTarget target, CancellationToken token = default)
    => AttachToTargetAsync(target.Id, token);

  public async Task<IScopedProtocolClient> AttachToTargetAsync(string targetId, CancellationToken token = default)
  {
    var client = await AttachAsync(token).ConfigureAwait(false);
    var attach = await client.SendCommandAsync(Target.AttachToTarget(new Target.TargetIDType(targetId), Flatten: true), null, token)
      .ConfigureAwait(false);
    return client.CreateScoped(attach.SessionId.Value);
  }

  private async Task PollTargetsLoopAsync(CancellationToken token)
  {
    while (!token.IsCancellationRequested)
    {
      try
      {
        var targets = await FetchTargetsAsync(token).ConfigureAwait(false);
        UpdateCollections(targets);
      }
      catch (OperationCanceledException)
      {
        // Normal cancellation on dispose
        break;
      }
      catch (Exception ex)
      {
        _logger.LogDebug(ex, "Failed to poll /json targets");
      }

      try
      {
        await Task.Delay(TimeSpan.FromSeconds(1), token).ConfigureAwait(false);
      }
      catch (OperationCanceledException)
      {
        break;
      }
    }
  }

  private async Task<IReadOnlyList<ChromiumTarget>> FetchTargetsAsync(CancellationToken token)
  {
    var httpUri = BuildHttpJsonUri("/json");
    using var response = await _httpClient.GetAsync(httpUri, token).ConfigureAwait(false);
    if (!response.IsSuccessStatusCode)
    {
      // Fallback to /json/list
      var listUri = BuildHttpJsonUri("/json/list");
      using var responseList = await _httpClient.GetAsync(listUri, token).ConfigureAwait(false);
      responseList.EnsureSuccessStatusCode();
      await using var stream2 = await responseList.Content.ReadAsStreamAsync(token).ConfigureAwait(false);
      var targets2 = await JsonSerializer.DeserializeAsync<ChromiumTarget[]>(stream2, cancellationToken: token).ConfigureAwait(false);
      return targets2 ?? Array.Empty<ChromiumTarget>();
    }

    await using var stream = await response.Content.ReadAsStreamAsync(token).ConfigureAwait(false);
    var targets = await JsonSerializer.DeserializeAsync<ChromiumTarget[]>(stream, cancellationToken: token).ConfigureAwait(false);
    return targets ?? Array.Empty<ChromiumTarget>();
  }

  private Uri BuildHttpJsonUri(string path)
  {
    var builder = new UriBuilder(DebuggingEndpoint)
    {
      Scheme = DebuggingEndpoint.Scheme == "wss" ? "https" : "http",
      Host = DebuggingEndpoint.Host,
      Port = DebuggingEndpoint.Port,
      Path = path,
      Query = string.Empty
    };
    return builder.Uri;
  }

  private void UpdateCollections(IReadOnlyList<ChromiumTarget> freshTargets)
  {
    lock (Targets)
    {
      // Remove missing
      for (var i = Targets.Count - 1; i >= 0; i--)
      {
        var existing = Targets[i];
        if (!freshTargets.Any(t => t.Id == existing.Id))
          Targets.RemoveAt(i);
      }

      // Add/update
      foreach (var t in freshTargets)
      {
        var existing = Targets.FirstOrDefault(x => x.Id == t.Id);
        if (existing is null)
          Targets.Add(t);
        else if (existing != t)
        {
          var index = Targets.IndexOf(existing);
          Targets[index] = t;
        }
      }

      // Update Pages
      var freshPages = freshTargets.Where(t => string.Equals(t.Type, "page", StringComparison.OrdinalIgnoreCase)).ToList();
      for (var i = Pages.Count - 1; i >= 0; i--)
      {
        var existing = Pages[i];
        if (!freshPages.Any(t => t.Id == existing.Id))
          Pages.RemoveAt(i);
      }

      foreach (var p in freshPages)
      {
        var existing = Pages.FirstOrDefault(x => x.Id == p.Id);
        if (existing is null)
          Pages.Add(p);
        else if (existing != p)
        {
          var index = Pages.IndexOf(existing);
          Pages[index] = p;
        }
      }
    }
  }

  public void Dispose()
  {
    try
    {
      _cts.Cancel();
      _httpClient.Dispose();
    }
    catch
    {
      // ignore
    }

    try
    {
      _process.Kill();
    }
    catch
    {
      // ignore
    }
  }
}
