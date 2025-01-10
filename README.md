# ChromeProtocol

[![NuGet version (ChromeProtocol.Runtime)](https://img.shields.io/nuget/v/ChromeProtocol.Runtime.svg?style=flat-square)](https://www.nuget.org/packages/ChromeProtocol.Runtime/)
!['main' Build status](../../actions/workflows/build.yml/badge.svg?branch=main)

A runtime library and schema code generation tools for Chrome DevTools Protocol support in C#/.NET.

## Features

- Asynchronous and synchronous APIs for commands execution
- Disposable event subscriptions, one-time subscriptions
- Code-generation of domains, commands, events, types from protocol's JSON schema
- Multiple CDP schema files support in generation pipeline with domains definitions merging
- .NET Standard 2.0 compatible

## How to use

### Installation
- `ChromeProtocol.Runtime`: contains all necessary run-time code to work through CDP.
- `ChromeProtocol.Domains`: contains pre-generated classes representing official CDP domains set.

### Usage

#### Create a browser client
```csharp
// A port should be the one used in --remote-debugging-port argument when launching Chrome
var debuggingEndpoint = new Uri("ws://127.0.0.1:1234");
var browserClient = new DefaultProtocolClient(debuggingEndpoint, new ConsoleLogger(...));
await browserClient.ConnectAsync();
```

#### Create a scoped client (a client for the specific session)
```csharp
var pageClient = browserClient.CreateScoped(sessionId);
```

#### Send commands
```csharp
// Send and wait for the response
var targets = await browserClient.SendCommandAsync(Domains.Target.GetTargets());
await pageClient.SendCommandAsync(Domains.Debugger.Enable());

// Just send and resume execution immediately
await pageClient.FireCommandAsync(Domains.Runtime.Evaluate("alert('Hello there')"));
```

#### Listen for events
```csharp
pageClient.SubscribeAsync<Domains.Debugger.Paused>(paused => {
    Console.WriteLine("paused called");
})
    
// Subscriptions are disposable
var subscription = pageClient.SubscribeAsync<Domains.Target.TargetInfoChanged>(changed => ...);
...
subscription.Dispose();

// Single use subscription
var subscription = pageClient.SubscribeOnce<Domains.Page.FrameNavigated>(navigated => {
    InitializeSomeStuff();
});
```

### Generate own domains

#### Using `dotnet cdp` tools

In case pre-generated domains from `ChromeProtocol.Domains` package are not enough for your use case, you can generate code by your own schema files.

To do so:
1. Install `dotnet cdp` command line into your solution/project tools via `dotnet tool install dotnet-cdp` (or `dotnet tool install -g dotnet-cdp` to install tools globally)
2. Prepare schema files. Chrome browser & JS schema could be obtained [here](https://github.com/ChromeDevTools/devtools-protocol/tree/master/json).
3. Execute generate command:
   ```
   > dotnet cdp generate js_protocol.json browser_protocol.json --namespace YourApp.Domains --output YourApp.Domains/SomeFolder/Generated
   ```
4. `dotnet cdp --help` will guide you with all available options:
   ```
   > dotnet cdp generate --help
   DESCRIPTION:
       Generates strongly-typed C# classes for domain types, events and commands from protocol definition files to be used with ChromeProtocol.
   
   USAGE:
       dotnet cdp generate <path> [OPTIONS]
   
   EXAMPLES:
       dotnet cdp generate js_protocol.json mono_protocol.json -n DevTools.Api.Generated -o ./out
   
   ARGUMENTS:
       <path>    Path to the .json file with the CDP schema to generate protocol definitions from
   
   OPTIONS:
                          DEFAULT                                                                         
       -h, --help                      Prints help information
       -n, --namespace    Generated    Namespace for the generated files
       -o, --output       Generated    Folder where generated files should be placed
       --clean            True         Should output folder be cleaned before performing generation or not
   ```

### Development

### Prerequisites

.NET SDK 8.0 or newer

### Build

`dotnet build` from solution folder

### Tests

Just run `dotnet test`:
```
dotnet test ChromeProtocol.Runtime.Tests
dotnet test ChromeProtocol.Tools.Tests
```
