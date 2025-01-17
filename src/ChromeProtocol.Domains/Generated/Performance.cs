// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  public static partial class Performance
  {
    /// <summary>Run-time execution metric.</summary>
    /// <param name="Name">Metric name.</param>
    /// <param name="Value">Metric value.</param>
    public record MetricType(
      [property: System.Text.Json.Serialization.JsonPropertyName("name")]
      string Name,
      [property: System.Text.Json.Serialization.JsonPropertyName("value")]
      double Value
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Current values of the metrics.</summary>
    /// <param name="MetricsProperty">Current values of the metrics.</param>
    /// <param name="Title">Timestamp title.</param>
    [ChromeProtocol.Core.MethodName("Performance.metrics")]
    public record Metrics(
      [property: System.Text.Json.Serialization.JsonPropertyName("metrics")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Performance.MetricType> MetricsProperty,
      [property: System.Text.Json.Serialization.JsonPropertyName("title")]
      string Title
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Disable collecting and reporting metrics.</summary>
    public static ChromeProtocol.Domains.Performance.DisableRequest Disable()    
    {
      return new ChromeProtocol.Domains.Performance.DisableRequest();
    }
    /// <summary>Disable collecting and reporting metrics.</summary>
    [ChromeProtocol.Core.MethodName("Performance.disable")]
    public record DisableRequest() : ChromeProtocol.Core.ICommand<DisableRequestResult>
    {
    }
    public record DisableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Enable collecting and reporting metrics.</summary>
    /// <param name="TimeDomain">Time domain to use for collecting and reporting duration metrics.</param>
    public static ChromeProtocol.Domains.Performance.EnableRequest Enable(string? TimeDomain = default)    
    {
      return new ChromeProtocol.Domains.Performance.EnableRequest(TimeDomain);
    }
    /// <summary>Enable collecting and reporting metrics.</summary>
    /// <param name="TimeDomain">Time domain to use for collecting and reporting duration metrics.</param>
    [ChromeProtocol.Core.MethodName("Performance.enable")]
    public record EnableRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("timeDomain")]
      string? TimeDomain = default
    ) : ChromeProtocol.Core.ICommand<EnableRequestResult>
    {
    }
    public record EnableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>
    /// Sets time domain to use for collecting and reporting duration metrics.<br/>
    /// Note that this must be called before enabling metrics collection. Calling<br/>
    /// this method while metrics collection is enabled returns an error.<br/>
    /// </summary>
    /// <param name="TimeDomain">Time domain</param>
    [System.Obsolete("This command marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.", false)]
    public static ChromeProtocol.Domains.Performance.SetTimeDomainRequest SetTimeDomain(string TimeDomain)    
    {
      return new ChromeProtocol.Domains.Performance.SetTimeDomainRequest(TimeDomain);
    }
    /// <summary>
    /// Sets time domain to use for collecting and reporting duration metrics.<br/>
    /// Note that this must be called before enabling metrics collection. Calling<br/>
    /// this method while metrics collection is enabled returns an error.<br/>
    /// </summary>
    /// <param name="TimeDomain">Time domain</param>
    [ChromeProtocol.Core.MethodName("Performance.setTimeDomain")]
    [System.Obsolete("This command marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.", false)]
    public record SetTimeDomainRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("timeDomain")]
      string TimeDomain
    ) : ChromeProtocol.Core.ICommand<SetTimeDomainRequestResult>
    {
    }
    [System.Obsolete("This command marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.", false)]
    public record SetTimeDomainRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Retrieve current values of run-time metrics.</summary>
    public static ChromeProtocol.Domains.Performance.GetMetricsRequest GetMetrics()    
    {
      return new ChromeProtocol.Domains.Performance.GetMetricsRequest();
    }
    /// <summary>Retrieve current values of run-time metrics.</summary>
    [ChromeProtocol.Core.MethodName("Performance.getMetrics")]
    public record GetMetricsRequest() : ChromeProtocol.Core.ICommand<GetMetricsRequestResult>
    {
    }
    /// <param name="Metrics">Current values for run-time metrics.</param>
    public record GetMetricsRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("metrics")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Performance.MetricType> Metrics
    ) : ChromeProtocol.Core.IType
    {
    }
  }
}
