using ChromeProtocol.Tools.CodeGeneration.Builders;
using ChromeProtocol.Tools.CodeGeneration.Emitting;
using ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;

namespace ChromeProtocol.Tools.CodeGeneration.Services;

public class XmlDocumentationDecorator
{
  public static CsharpTypeDeclBuilder<T1, T2> AddXmlDocs<T1, T2>(CsharpTypeDeclBuilder<T1, T2> domainBuilder, ValidatedDomain domain)
    where T1 : CsharpTypeDeclBuilder<T1, T2>
    where T2 : CsharpTypeDecl, new()
  {
    return domainBuilder
      .ApplyIf(!string.IsNullOrEmpty(domain.Description), _ => _
        .XmlComment(xmlBuilder => xmlBuilder.Summary(domain.Description)));
  }

  public static CsharpTypeDeclBuilder<T1, T2> AddXmlDocs<T1, T2>(CsharpTypeDeclBuilder<T1, T2> typeBuilder, ValidatedType type, Func<string, string> propertyNameTransformer)
    where T1 : CsharpTypeDeclBuilder<T1, T2>
    where T2 : CsharpTypeDecl, new()
  {
    return typeBuilder
      .XmlComment(xmlBuilder =>
      {
        if (!string.IsNullOrEmpty(type.Description))
          xmlBuilder.Summary(type.Description);

        var documentedProperties = type.Properties
          .Where(property => !string.IsNullOrEmpty(property.Description))
          .OrderBy(property => property.Optional);

        foreach (var property in documentedProperties)
          xmlBuilder.Param(propertyNameTransformer(property.Name), property.Description);
      });
  }

  public static CsharpTypeDeclBuilder<T1, T2> AddXmlDocs<T1, T2>(CsharpTypeDeclBuilder<T1, T2> eventBuilder, ValidatedEvent @event, Func<string, string> propertyNameTransformer)
    where T1 : CsharpTypeDeclBuilder<T1, T2>
    where T2 : CsharpTypeDecl, new()
  {
    return eventBuilder
      .XmlComment(xmlBuilder =>
      {
        if (!string.IsNullOrEmpty(@event.Description))
          xmlBuilder.Summary(@event.Description);

        var documentedParameters = @event.Parameters
          .Where(property => !string.IsNullOrEmpty(property.Description))
          .OrderBy(property => property.Optional);

        foreach (var parameter in documentedParameters)
          xmlBuilder.Param(propertyNameTransformer(parameter.Name), parameter.Description);
      });
  }

  public static void AddCommandRequestXmlDocs<T1, T2>(
    CsharpTypeDeclBuilder<T1, T2> requestBuilder,
    ValidatedCommand command,
    Func<string, string> propertyNameTransformer
  ) where T1 : CsharpTypeDeclBuilder<T1, T2> where T2 : CsharpTypeDecl, new()
  {
    requestBuilder
      .XmlComment(xmlBuilder =>
      {
        if (!string.IsNullOrEmpty(command.Description))
          xmlBuilder.Summary(command.Description);

        var documentedParameters = command.Parameters
          .Where(property => !string.IsNullOrEmpty(property.Description))
          .OrderBy(property => property.Optional);

        foreach (var parameter in documentedParameters)
          xmlBuilder.Param(propertyNameTransformer(parameter.Name), parameter.Description);
      });
  }

  public static void AddCommandRequestXmlDocs(
    CsharpMethodDeclBuilder requestBuilder,
    ValidatedCommand command,
    Func<string, string> propertyNameTransformer
  )
  {
    requestBuilder
      .XmlComment(xmlBuilder =>
      {
        if (!string.IsNullOrEmpty(command.Description))
          xmlBuilder.Summary(command.Description);

        var documentedParameters = command.Parameters
          .Where(property => !string.IsNullOrEmpty(property.Description))
          .OrderBy(property => property.Optional);

        foreach (var parameter in documentedParameters)
          xmlBuilder.Param(propertyNameTransformer(parameter.Name), parameter.Description);
      });
  }

  public static void AddCommandResultXmlDocs<T1, T2>(
    CsharpTypeDeclBuilder<T1, T2> responseBuilder,
    ValidatedCommand command,
    Func<string, string> propertyNameTransformer
  ) where T1 : CsharpTypeDeclBuilder<T1, T2> where T2 : CsharpTypeDecl, new()
  {
    responseBuilder
      .XmlComment(xmlBuilder =>
      {
        var documentedReturns = command.Returns
          .Where(property => !string.IsNullOrEmpty(property.Description))
          .OrderBy(property => property.Optional);

        foreach (var parameter in documentedReturns)
          xmlBuilder.Param(propertyNameTransformer(parameter.Name), parameter.Description);
      });
  }
}
