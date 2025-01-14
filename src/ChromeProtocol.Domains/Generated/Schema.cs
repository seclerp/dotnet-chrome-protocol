// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  /// <summary>This domain is deprecated.</summary>
  [System.Obsolete("This domain marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.", false)]
  public static partial class Schema
  {
    /// <summary>Description of the protocol domain.</summary>
    /// <param name="Name">Domain name.</param>
    /// <param name="Version">Domain version.</param>
    public record DomainType(
      [property: System.Text.Json.Serialization.JsonPropertyName("name")]
      string Name,
      [property: System.Text.Json.Serialization.JsonPropertyName("version")]
      string Version
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Returns supported domains.</summary>
    public static ChromeProtocol.Domains.Schema.GetDomainsRequest GetDomains()    
    {
      return new ChromeProtocol.Domains.Schema.GetDomainsRequest();
    }
    /// <summary>Returns supported domains.</summary>
    [ChromeProtocol.Core.MethodName("Schema.getDomains")]
    public record GetDomainsRequest() : ChromeProtocol.Core.ICommand<GetDomainsRequestResult>
    {
    }
    /// <param name="Domains">List of supported domains.</param>
    public record GetDomainsRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("domains")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Schema.DomainType> Domains
    ) : ChromeProtocol.Core.IType
    {
    }
  }
}
