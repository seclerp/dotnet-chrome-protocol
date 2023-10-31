using System.Text.Json;
using ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;
using ChromeProtocol.Tools.Schema;
using ChromeProtocol.Tools.Schema.Models;
using ChromeProtocol.Tools.Services;
using Type = ChromeProtocol.Tools.Schema.Models.Type;

namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Steps;

public class DefinitionFilesValidationStep : ICodeGenerationPipelineStep<CodeGenerationContext>
{
  private readonly ProtocolSchemaValidator _validator = new();

  public async Task InvokeAsync(CodeGenerationPipeline<CodeGenerationContext> next, CodeGenerationContext ctx)
  {
    var result = await ValidateAsync(ctx).ConfigureAwait(false);
    if (result.Errors.Any())
    {
      foreach (var error in result.Errors)
      {
        ctx.Errors.Add(error);
      }

      return;
    }

    ctx.ValidatedDefinitions = result.ParsedDefinitions;

    await next.Invoke(ctx).ConfigureAwait(false);
  }

  private async Task<ValidationResult> ValidateAsync(CodeGenerationContext ctx)
  {
    var successResults = new LinkedList<DefinitionFile>();
    var errorResults = new LinkedList<CodeGenerationError>();

    foreach (var fileName in ctx.DefinitionFiles)
    {
      if (!File.Exists(fileName))
      {
        errorResults.AddLast(new CodeGenerationError("Error", $"Protocol definition file '{fileName}' doesn't exist."));
        continue;
      }

      await using var jsonStream = File.OpenRead(fileName);
      var jsonDocument = await JsonDocument.ParseAsync(jsonStream).ConfigureAwait(false);
      var (valid, errors) = await _validator.TryValidateAsync(fileName, jsonDocument).ConfigureAwait(false);
      if (valid)
      {
        var parsed = SchemaSerializer.ParseDefinition(jsonDocument);
        successResults.AddLast(new DefinitionFile(fileName, Map(parsed)));
      }
      foreach (var error in errors)
      {
        errorResults.AddLast(new CodeGenerationError(fileName, error));
      }
    }

    return new ValidationResult(successResults, errorResults);
  }

  private static ValidatedDefinition Map(Definition definition) =>
    new(definition.Version, definition.Domains.Select(Map).ToArray());

  private static ValidatedDomain Map(Domain domain) =>
    new(
      domain.Name,
      domain.Description ?? string.Empty,
      domain.Dependencies ?? Array.Empty<string>(),
      domain.Types?.Select(Map).ToArray() ?? Array.Empty<ValidatedType>(),
      domain.Events?.Select(Map).ToArray() ?? Array.Empty<ValidatedEvent>(),
      domain.Commands?.Select(Map).ToArray() ?? Array.Empty<ValidatedCommand>(),
      domain.Experimental ?? false,
      domain.Deprecated ?? false
    );

  private static ValidatedType Map(Type type) =>
    new(
      type.Id,
      type.Description ?? string.Empty,
      type.Kind,
      type.Properties?.Select(Map).ToArray() ?? Array.Empty<ValidatedProperty>(),
      type.Enum ?? Array.Empty<string>(),
      type.Experimental ?? false,
      type.Deprecated ?? false
    );

  private static ValidatedEvent Map(Event @event) =>
    new(
      @event.Name,
      @event.Description ?? string.Empty,
      @event.Parameters?.Select(Map).ToArray() ?? Array.Empty<ValidatedProperty>(),
      @event.Experimental ?? false,
      @event.Deprecated ?? false
    );

  private static ValidatedCommand Map(Command command) =>
    new(
      command.Name,
      command.Description ?? string.Empty,
      command.Parameters?.Select(Map).ToArray() ?? Array.Empty<ValidatedProperty>(),
      command.Returns?.Select(Map).ToArray() ?? Array.Empty<ValidatedProperty>(),
      command.Experimental ?? false,
      command.Deprecated ?? false
    );

  private static ValidatedProperty Map(Property property) =>
    new(
      property.Name,
      property.Description ?? string.Empty,
      property.Ref ?? string.Empty,
      property.Optional ?? false,
      property.Kind,
      property.Enum ?? Array.Empty<string>(),
      property.Items is null ? null : Map(property.Items),
      property.Experimental ?? false,
      property.Deprecated ?? false
    );

  private static ValidatedItems Map(Items items) =>
    new(
      items.Type,
      items.Ref
    );

  private record ValidationResult(
    IReadOnlyCollection<DefinitionFile> ParsedDefinitions,
    IReadOnlyCollection<CodeGenerationError> Errors
  );
}
