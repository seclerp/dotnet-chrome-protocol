using System.Diagnostics;
using ChromeProtocol.Core;
using ChromeProtocol.Tools.CodeGeneration.Builders;
using ChromeProtocol.Tools.CodeGeneration.Emitting;
using ChromeProtocol.Tools.CodeGeneration.Models;
using ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;
using ChromeProtocol.Tools.CodeGeneration.Services;
using ChromeProtocol.Tools.Extensions;
using ChromeProtocol.Tools.Schema.Models;
using Newtonsoft.Json;

namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Steps;

// ReSharper disable once VariableHidesOuterVariable
public class GenerateCsharpStep : ICodeGenerationPipelineStep<CodeGenerationContext>
{
  public async Task InvokeAsync(CodeGenerationPipeline<CodeGenerationContext> next, CodeGenerationContext ctx)
  {
    if (ctx.MergedDomains is null) return;

    ctx.GeneratedSources = ctx.MergedDomains
      .Select(domain => (domain.Name, GenerateDomainClassSources(domain, ctx.Namespace)))
      .ToList();

    await next.Invoke(ctx).ConfigureAwait(false);
  }

  private string GenerateDomainClassSources(ValidatedDomain domain, string @namespace)
  {
    var file = CsharpFileBuilder
      .Create()
      .Namespace(@namespace, nsBuilder => GenerateDomain(domain, @namespace, nsBuilder))
      .Build();

    var emitter = new SourceCodeEmitter();
    file.Emit(emitter, new EmissionState());
    return emitter.Finish();
  }

  private static CsharpNamespaceDeclBuilder GenerateDomain(ValidatedDomain domain, string @namespace, CsharpNamespaceDeclBuilder nsBuilder) =>
    nsBuilder.Class(domain.Name, classBuilder => classBuilder
      .Modifiers("public", "static", "partial")
      .ApplyIf(!string.IsNullOrEmpty(domain.Description), _ => _
        .XmlComment(xmlBuilder => xmlBuilder.Summary(domain.Description)))
      .ApplyIf(domain.Deprecated, _ => MarkDeprecated(_, "domain"))
      .Apply(_ =>
      {
        foreach (var type in domain.Types)
          GenerateType(type, domain, @namespace, _);
        foreach (var @event in domain.Events)
          GenerateEvent(@event, domain, @namespace, _);
        foreach (var command in domain.Commands)
          GenerateCommand(command, domain, @namespace, _);
      })
      .Apply(_ => XmlDocumentationDecorator.AddXmlDocs(_, domain))
    );

  private static CsharpClassDeclBuilder GenerateType(ValidatedType type, ValidatedDomain domain, string @namespace, CsharpClassDeclBuilder classBuilder)
  {
    return type switch
    {
      // { Kind: TypeKind.Object } =>
      //   classBuilder.Record(CsharpNameResolver.Resolve(type.Id, ItemKind.TypeName, classBuilder.Node.Name),
      //     recordBuilder => recordBuilder
      //       .Modifiers("public")
      //       .ApplyIf(type.Deprecated, _ => MarkDeprecated(_, "type"))
      //       .Parameters(paramsBuilder =>
      //         GenerateParameters(paramsBuilder, type.Properties, domain, @namespace, recordBuilder.Node.Name))
      //       .Inherit(CsharpTypeInfo.FromTypeName("ChromeProtocol.Core", nameof(IType)))
      //       .Apply(_ =>
      //         XmlDocumentationDecorator.AddXmlDocs(_, type, propName =>
      //           CsharpNameResolver.Resolve(propName, ItemKind.PropertyName, recordBuilder.Node.Name)))),
      // { Kind: TypeKind.String, Enum: not null } => throw new NotImplementedException(),
      { Kind: not TypeKind.Object and not TypeKind.Array } =>
        classBuilder.Record(CsharpNameResolver.Resolve(type.Id, ItemKind.TypeName, classBuilder.Node.Name),
          recordBuilder =>
          {
            var valueType = CsharpTypeResolver.Resolve(@namespace, domain.Name, null, type.Kind, null, false);
            recordBuilder
              .Attribute(CsharpTypeInfo.FromTypeName("Newtonsoft.Json", nameof(JsonConverter)),
                attr => attr.Arguments("typeof(JetBrains.Wasm.Debugger.ChromeProtocol.Messaging.Json.PrimitiveTypeConverter)"))
              .Modifiers("public")
              .ApplyIf(type.Deprecated, _ => MarkDeprecated(_, "type"))
              .Parameters(paramsBuilder => paramsBuilder.Parameter("Value", valueType))
              .Inherit(CsharpTypeInfo.FromGenericType("ChromeProtocol.Core", "PrimitiveType", valueType), p => p.Argument("Value"))
              .Apply(_ =>
                XmlDocumentationDecorator.AddXmlDocs(_, type, _ => throw new UnreachableException()));
          }),
      _ =>
        classBuilder.Record(CsharpNameResolver.Resolve(type.Id, ItemKind.TypeName, classBuilder.Node.Name),
          recordBuilder => recordBuilder
            .Modifiers("public")
            .ApplyIf(type.Deprecated, _ => MarkDeprecated(_, "type"))
            .Parameters(paramsBuilder =>
              GenerateParameters(paramsBuilder, type.Properties, domain, @namespace, recordBuilder.Node.Name))
            .Inherit(CsharpTypeInfo.FromTypeName("ChromeProtocol.Core", nameof(IType)))
            .Apply(_ =>
              XmlDocumentationDecorator.AddXmlDocs(_, type, propName =>
                CsharpNameResolver.Resolve(propName, ItemKind.PropertyName, recordBuilder.Node.Name)))),
    };
  }

  private static CsharpClassDeclBuilder GenerateEvent(ValidatedEvent @event, ValidatedDomain domain, string @namespace, CsharpClassDeclBuilder classBuilder) =>
    classBuilder.Record(CsharpNameResolver.Resolve(@event.Name, ItemKind.EventName, classBuilder.Node.Name), recordBuilder => recordBuilder
      .Attribute(CsharpTypeInfo.FromTypeName("ChromeProtocol.Core", "MethodName"), _ => _.Arguments($"\"{domain.Name}.{@event.Name}\""))
      .Modifiers("public")
      .ApplyIf(@event.Deprecated, _ => MarkDeprecated(_, "event"))
      .Parameters(paramsBuilder => GenerateParameters(paramsBuilder, @event.Parameters, domain, @namespace, recordBuilder.Node.Name))
      .Inherit(CsharpTypeInfo.FromTypeName("ChromeProtocol.Core", nameof(IEvent)))
      .Apply(_ =>
        XmlDocumentationDecorator.AddXmlDocs(_, @event, propName =>
          CsharpNameResolver.Resolve(propName, ItemKind.PropertyName, recordBuilder.Node.Name))));

  private static CsharpClassDeclBuilder GenerateCommand(ValidatedCommand command, ValidatedDomain domain, string @namespace, CsharpClassDeclBuilder classBuilder)
  {
    var commandCsharpName = CsharpNameResolver.Resolve(command.Name, ItemKind.CommandName, classBuilder.Node.Name);
    var factoryMethodName = command.Name.ToTitleCase();
    var commandResultTypeName = $"{commandCsharpName}Result";

    return classBuilder
      .Method(factoryMethodName, methodBuilder => methodBuilder
        .Modifiers("public", "static")
        .ApplyIf(command.Deprecated, _ => MarkDeprecated(_, "command"))
        .ReturnType(CsharpTypeInfo.FromTypeName(@namespace, $"{domain.Name}.{commandCsharpName}"))
        .Parameters(paramsBuilder => GenerateParameters(paramsBuilder, command.Parameters, domain, @namespace, methodBuilder.Node.Name))
        .Code($"return new {methodBuilder.Node.ReturnType.FullName}({string.Join(", ", methodBuilder.Node.Parameters.Select(p => p.Name))});")
        .Apply(_ =>
          XmlDocumentationDecorator.AddCommandRequestXmlDocs(_, command, propName =>
            CsharpNameResolver.Resolve(propName, ItemKind.PropertyName, methodBuilder.Node.Name))))

      .Record(commandCsharpName, recordBuilder => recordBuilder
        .Attribute(CsharpTypeInfo.FromTypeName("ChromeProtocol.Core", "MethodName"), _ => _.Arguments($"\"{domain.Name}.{command.Name}\""))
        .Modifiers("public")
        .ApplyIf(command.Deprecated, _ => MarkDeprecated(_, "command"))
        .Parameters(paramsBuilder => GenerateParameters(paramsBuilder, command.Parameters, domain, @namespace, recordBuilder.Node.Name))
        .Inherit(CsharpTypeInfo.FromTypeName("ChromeProtocol.Core", $"ICommand<{commandResultTypeName}>"))
        .Apply(_ =>
          XmlDocumentationDecorator.AddCommandRequestXmlDocs(_, command, propName =>
            CsharpNameResolver.Resolve(propName, ItemKind.PropertyName, recordBuilder.Node.Name))))

      .Record(commandResultTypeName, recordBuilder => recordBuilder
        .Modifiers("public")
        .ApplyIf(command.Deprecated, _ => MarkDeprecated(_, "command"))
        .Parameters(paramsBuilder => GenerateParameters(paramsBuilder, command.Returns, domain, @namespace, recordBuilder.Node.Name))
        .Inherit(CsharpTypeInfo.FromTypeName("ChromeProtocol.Core", nameof(IType)))
        .Apply(_ =>
          XmlDocumentationDecorator.AddCommandResultXmlDocs(_, command, propName =>
            CsharpNameResolver.Resolve(propName, ItemKind.PropertyName, recordBuilder.Node.Name))));
  }

  private static void GenerateParameters(CsharpRecordParametersListBuilder paramsBuilder,
    IEnumerable<ValidatedProperty> parameters, ValidatedDomain domain, string @namespace, string enclosingTypeName)
  {
    foreach (var property in parameters.OrderBy(property => property.Optional))
      paramsBuilder.Parameter(
        CsharpNameResolver.Resolve(property.Name, ItemKind.PropertyName, enclosingTypeName),
        CsharpTypeResolver.Resolve(@namespace, domain.Name, property.Ref, property.Kind, property.Items,
          property.Optional),
        propBuilder => GenerateParameter(propBuilder, property));
  }

  private static void GenerateParameters(CsharpParametersListBuilder paramsBuilder,
    IEnumerable<ValidatedProperty> parameters, ValidatedDomain domain, string @namespace, string enclosingTypeName)
  {
    foreach (var property in parameters.OrderBy(property => property.Optional))
      paramsBuilder.Parameter(
        CsharpNameResolver.Resolve(property.Name, ItemKind.PropertyName, enclosingTypeName),
        CsharpTypeResolver.Resolve(@namespace, domain.Name, property.Ref, property.Kind, property.Items,
          property.Optional),
        propBuilder => GenerateParameter(propBuilder, property));
  }

  private static CsharpRecordParameterBuilder GenerateParameter(CsharpRecordParameterBuilder propBuilder, ValidatedProperty parameter) =>
    propBuilder
      .ApplyIf(parameter.Deprecated, MarkDeprecated)
      .ApplyIf(parameter.Optional, _ => _.DefaultValue("default"))
      .Attribute(CsharpTypeInfo.FromTypeName("Newtonsoft.Json", "JsonProperty"), attrBuilder => attrBuilder
        .Target("property")
        .Arguments($"\"{parameter.Name}\""));

  private static CsharpMethodParameterBuilder GenerateParameter(CsharpMethodParameterBuilder propBuilder, ValidatedProperty parameter) =>
    propBuilder
      .ApplyIf(parameter.Deprecated, MarkDeprecated)
      .ApplyIf(parameter.Optional, _ => _.DefaultValue("default"));

  private static CsharpTypeDeclBuilder<T1, T2> MarkDeprecated<T1, T2>(CsharpTypeDeclBuilder<T1, T2> builder, string presentableKind)
    where T1 : CsharpTypeDeclBuilder<T1, T2>
    where T2 : CsharpTypeDecl, new() =>
    builder.Attribute(CsharpTypeInfo.FromTypeName("System", "Obsolete"), attrBuilder => attrBuilder
      .Arguments(
        $"\"This {presentableKind} marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.\"",
        "false"));

  private static CsharpMethodDeclBuilder MarkDeprecated(CsharpMethodDeclBuilder builder, string presentableKind) =>
    builder.Attribute(CsharpTypeInfo.FromTypeName("System", "Obsolete"), attrBuilder => attrBuilder
      .Arguments(
        $"\"This {presentableKind} marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.\"",
        "false"));

  private static CsharpRecordParameterBuilder MarkDeprecated(CsharpRecordParameterBuilder builder) =>
    builder.Attribute(CsharpTypeInfo.FromTypeName("System", "Obsolete"), attrBuilder => attrBuilder
      .Target("property")
      .Arguments(
        "\"This property marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.\"",
        "false"));

  private static CsharpMethodParameterBuilder MarkDeprecated(CsharpMethodParameterBuilder builder) =>
    builder.Attribute(CsharpTypeInfo.FromTypeName("System", "Obsolete"), attrBuilder => attrBuilder
      .Target("property")
      .Arguments(
        "\"This property marked as deprecated in the corresponding CDP definition schema. It may be removed in the future releases.\"",
        "false"));
}
