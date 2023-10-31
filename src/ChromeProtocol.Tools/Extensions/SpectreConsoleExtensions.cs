// ReSharper disable once CheckNamespace
namespace Spectre.Console.Cli;

public static class SpectreConsoleExtensions
{
  /// <summary>
  /// Adds an example of how to use the command.
  /// </summary>
  /// <param name="config">Instance of <see cref="ICommandConfigurator"/>.</param>
  /// <param name="args">The example arguments.</param>
  /// <returns>The same <see cref="ICommandConfigurator"/> instance so that multiple calls can be chained.</returns>
  public static ICommandConfigurator WithExample(this ICommandConfigurator config, params string[] args) =>
    config.WithExample(args);
}
