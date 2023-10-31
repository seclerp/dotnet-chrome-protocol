using ChromeProtocol.Tools.CodeGeneration.Pipeline.Models;

namespace ChromeProtocol.Tools.CodeGeneration.Pipeline.Steps;

public class MergeDefinitionsStep : ICodeGenerationPipelineStep<CodeGenerationContext>
{
  public Task InvokeAsync(CodeGenerationPipeline<CodeGenerationContext> next, CodeGenerationContext ctx)
  {
    var entriesDictionary = new Dictionary<string, DomainItemDescriptor>();
    var entries = ctx.ValidatedDefinitions
      .SelectMany(definitionFile => definitionFile.Definition.Domains
        .SelectMany(domain => CollectEntries(definitionFile.FileName, domain)));

    foreach (var entry in entries)
    {
      var fullName = $"{entry.Domain}.{entry.Name}";
      if (entriesDictionary.TryGetValue(fullName, out var value))
      {
        ctx.Errors.Add(new CodeGenerationError(entry.DefinitionFile, $"Am entry with name '{fullName}' was already declared before as {value.Kind.ToString()} at '{Path.GetFileName(value.DefinitionFile)}'"));
        return Task.CompletedTask;
      }

      entriesDictionary.Add(fullName, entry);
    }

    ctx.MergedDomains = entriesDictionary.Values
      .Select(defRef => defRef.Domain)
      .Distinct()
      .GroupBy(domain => domain.Name)
      .Select(group => MergeDomains(group.ToList()))
      .ToArray();

    return next.Invoke(ctx);
  }

  private ValidatedDomain MergeDomains(IReadOnlyCollection<ValidatedDomain> items) =>
    items.Aggregate((acc, next) => acc with
    {
      Description = $"{acc.Description}\n{next.Description}",
      Types = acc.Types.Union(next.Types).ToArray(),
      Events = acc.Events.Union(next.Events).ToArray(),
      Commands = acc.Commands?.Union(next.Commands).ToArray() ?? next.Commands,
      Dependencies = acc.Dependencies?.Union(next.Dependencies).ToArray() ?? next.Dependencies,
      Deprecated = acc.Deprecated | next.Deprecated,
      Experimental = acc.Experimental | next.Experimental
    });

  private IEnumerable<DomainItemDescriptor> CollectEntries(string definitionFileName, ValidatedDomain domain)
  {
    foreach (var type in domain.Types)
      yield return new DomainItemDescriptor(definitionFileName, domain, type.Id, EntryKind.Type);

    foreach (var @event in domain.Events)
      yield return new DomainItemDescriptor(definitionFileName, domain, @event.Name, EntryKind.Event);

    foreach (var command in domain.Commands)
      yield return new DomainItemDescriptor(definitionFileName, domain, command.Name, EntryKind.Command);
  }

  private record DomainItemDescriptor(string DefinitionFile, ValidatedDomain Domain, string Name, EntryKind Kind);

  private enum EntryKind
  {
    Type,
    Event,
    Command
  }
}
