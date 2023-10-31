using ChromeProtocol.Tools.Commands;
using ChromeProtocol.Tools.Logging;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
  config.Settings.Registrar.RegisterInstance<ILogger>(new SpectreLogger());
  config.Settings.Registrar.RegisterInstance<IErrorFormatter>(new SpectreErrorFormatter());
  config.Settings.ApplicationName = "dotnet cdp";
  config.AddCommand<GenerateCommand>("generate")
    .WithDescription(
      "Generates strongly-typed C# classes for domain types, events and commands from protocol definition files to be used with ChromeProtocol.")
    .WithExample("generate", "js_protocol.json", "mono_protocol.json", "-n", "DevTools.Api.Generated", "-o", "./out");
  config.SetExceptionHandler(ex => AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything));
});
return app.Run(args);
