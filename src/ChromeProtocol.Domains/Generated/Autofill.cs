// <auto-generated />
#nullable enable

using ChromeProtocol.Core;

namespace ChromeProtocol.Domains
{
  /// <summary>Defines commands and events for Autofill.</summary>
  public static partial class Autofill
  {
    /// <param name="Number">16-digit credit card number.</param>
    /// <param name="Name">Name of the credit card owner.</param>
    /// <param name="ExpiryMonth">2-digit expiry month.</param>
    /// <param name="ExpiryYear">4-digit expiry year.</param>
    /// <param name="Cvc">3-digit card verification code.</param>
    public record CreditCardType(
      [property: Newtonsoft.Json.JsonProperty("number")]
      string Number,
      [property: Newtonsoft.Json.JsonProperty("name")]
      string Name,
      [property: Newtonsoft.Json.JsonProperty("expiryMonth")]
      string ExpiryMonth,
      [property: Newtonsoft.Json.JsonProperty("expiryYear")]
      string ExpiryYear,
      [property: Newtonsoft.Json.JsonProperty("cvc")]
      string Cvc
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <param name="Name">address field name, for example GIVEN_NAME.</param>
    /// <param name="Value">address field value, for example Jon Doe.</param>
    public record AddressFieldType(
      [property: Newtonsoft.Json.JsonProperty("name")]
      string Name,
      [property: Newtonsoft.Json.JsonProperty("value")]
      string Value
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>A list of address fields.</summary>
    public record AddressFieldsType(
      [property: Newtonsoft.Json.JsonProperty("fields")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Autofill.AddressFieldType> Fields
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <param name="Fields">fields and values defining an address.</param>
    public record AddressType(
      [property: Newtonsoft.Json.JsonProperty("fields")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Autofill.AddressFieldType> Fields
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>
    /// Defines how an address can be displayed like in chrome://settings/addresses.<br/>
    /// Address UI is a two dimensional array, each inner array is an &quot;address information line&quot;, and when rendered in a UI surface should be displayed as such.<br/>
    /// The following address UI for instance:<br/>
    /// [[{name: &quot;GIVE_NAME&quot;, value: &quot;Jon&quot;}, {name: &quot;FAMILY_NAME&quot;, value: &quot;Doe&quot;}], [{name: &quot;CITY&quot;, value: &quot;Munich&quot;}, {name: &quot;ZIP&quot;, value: &quot;81456&quot;}]]<br/>
    /// should allow the receiver to render:<br/>
    /// Jon Doe<br/>
    /// Munich 81456<br/>
    /// </summary>
    /// <param name="AddressFields">A two dimension array containing the repesentation of values from an address profile.</param>
    public record AddressUIType(
      [property: Newtonsoft.Json.JsonProperty("addressFields")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Autofill.AddressFieldsType> AddressFields
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Specified whether a filled field was done so by using the html autocomplete attribute or autofill heuristics.</summary>
    [Newtonsoft.Json.JsonConverter(typeof(PrimitiveTypeConverter))]
    public record FillingStrategyType(
      string Value
    ) : ChromeProtocol.Core.PrimitiveType<string>(Value)
    {
    }
    /// <param name="HtmlType">The type of the field, e.g text, password etc.</param>
    /// <param name="Id">the html id</param>
    /// <param name="Name">the html name</param>
    /// <param name="Value">the field value</param>
    /// <param name="AutofillType">The actual field type, e.g FAMILY_NAME</param>
    /// <param name="FillingStrategy">The filling strategy</param>
    public record FilledFieldType(
      [property: Newtonsoft.Json.JsonProperty("htmlType")]
      string HtmlType,
      [property: Newtonsoft.Json.JsonProperty("id")]
      string Id,
      [property: Newtonsoft.Json.JsonProperty("name")]
      string Name,
      [property: Newtonsoft.Json.JsonProperty("value")]
      string Value,
      [property: Newtonsoft.Json.JsonProperty("autofillType")]
      string AutofillType,
      [property: Newtonsoft.Json.JsonProperty("fillingStrategy")]
      ChromeProtocol.Domains.Autofill.FillingStrategyType FillingStrategy
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Emitted when an address form is filled.</summary>
    /// <param name="FilledFields">Information about the fields that were filled</param>
    /// <param name="AddressUi">
    /// An UI representation of the address used to fill the form.<br/>
    /// Consists of a 2D array where each child represents an address/profile line.<br/>
    /// </param>
    [ChromeProtocol.Core.MethodName("Autofill.addressFormFilled")]
    public record AddressFormFilled(
      [property: Newtonsoft.Json.JsonProperty("filledFields")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Autofill.FilledFieldType> FilledFields,
      [property: Newtonsoft.Json.JsonProperty("addressUi")]
      ChromeProtocol.Domains.Autofill.AddressUIType AddressUi
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>
    /// Trigger autofill on a form identified by the fieldId.<br/>
    /// If the field and related form cannot be autofilled, returns an error.<br/>
    /// </summary>
    /// <param name="FieldId">Identifies a field that serves as an anchor for autofill.</param>
    /// <param name="Card">Credit card information to fill out the form. Credit card data is not saved.</param>
    /// <param name="FrameId">Identifies the frame that field belongs to.</param>
    public static ChromeProtocol.Domains.Autofill.TriggerRequest Trigger(ChromeProtocol.Domains.DOM.BackendNodeIdType FieldId, ChromeProtocol.Domains.Autofill.CreditCardType Card, ChromeProtocol.Domains.Page.FrameIdType? FrameId = default)    
    {
      return new ChromeProtocol.Domains.Autofill.TriggerRequest(FieldId, Card, FrameId);
    }
    /// <summary>
    /// Trigger autofill on a form identified by the fieldId.<br/>
    /// If the field and related form cannot be autofilled, returns an error.<br/>
    /// </summary>
    /// <param name="FieldId">Identifies a field that serves as an anchor for autofill.</param>
    /// <param name="Card">Credit card information to fill out the form. Credit card data is not saved.</param>
    /// <param name="FrameId">Identifies the frame that field belongs to.</param>
    [ChromeProtocol.Core.MethodName("Autofill.trigger")]
    public record TriggerRequest(
      [property: Newtonsoft.Json.JsonProperty("fieldId")]
      ChromeProtocol.Domains.DOM.BackendNodeIdType FieldId,
      [property: Newtonsoft.Json.JsonProperty("card")]
      ChromeProtocol.Domains.Autofill.CreditCardType Card,
      [property: Newtonsoft.Json.JsonProperty("frameId")]
      ChromeProtocol.Domains.Page.FrameIdType? FrameId = default
    ) : ChromeProtocol.Core.ICommand<TriggerRequestResult>
    {
    }
    public record TriggerRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Set addresses so that developers can verify their forms implementation.</summary>
    public static ChromeProtocol.Domains.Autofill.SetAddressesRequest SetAddresses(System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Autofill.AddressType> Addresses)    
    {
      return new ChromeProtocol.Domains.Autofill.SetAddressesRequest(Addresses);
    }
    /// <summary>Set addresses so that developers can verify their forms implementation.</summary>
    [ChromeProtocol.Core.MethodName("Autofill.setAddresses")]
    public record SetAddressesRequest(
      [property: Newtonsoft.Json.JsonProperty("addresses")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Autofill.AddressType> Addresses
    ) : ChromeProtocol.Core.ICommand<SetAddressesRequestResult>
    {
    }
    public record SetAddressesRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Disables autofill domain notifications.</summary>
    public static ChromeProtocol.Domains.Autofill.DisableRequest Disable()    
    {
      return new ChromeProtocol.Domains.Autofill.DisableRequest();
    }
    /// <summary>Disables autofill domain notifications.</summary>
    [ChromeProtocol.Core.MethodName("Autofill.disable")]
    public record DisableRequest() : ChromeProtocol.Core.ICommand<DisableRequestResult>
    {
    }
    public record DisableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Enables autofill domain notifications.</summary>
    public static ChromeProtocol.Domains.Autofill.EnableRequest Enable()    
    {
      return new ChromeProtocol.Domains.Autofill.EnableRequest();
    }
    /// <summary>Enables autofill domain notifications.</summary>
    [ChromeProtocol.Core.MethodName("Autofill.enable")]
    public record EnableRequest() : ChromeProtocol.Core.ICommand<EnableRequestResult>
    {
    }
    public record EnableRequestResult() : ChromeProtocol.Core.IType
    {
    }
  }
}
