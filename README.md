# ChromeProtocol

!['main' Build status](../../actions/workflows/build.yml/badge.svg?branch=main)

A runtime library and schema code generation tools for Chrome DevTools Protocol support in C#/.NET.

## Features

- Code-generation of domains, commands, events, types from protocol's JSON schema
- Asynchronous and synchronous APIs for commands execution
- Disposable event subscriptions, one-time subscriptions
- Zero third-party runtime dependencies, works over plain `System.Net.WebSockets.ClientWebSocket`
- Multiple CDP schema files support in generation pipeline
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
```

### Create a scoped client (a client for specific session)
```csharp
var pageClient = browserClient.CreateScoped(sessionId);
```

### Send commands
```csharp
// Send and wait for response
var targets = await browserClient.SendCommandAsync(Domains.Target.GetTargets());
await pageClient.SendCommandAsync(Domains.Debugger.Enable());

// Just send and resume execution immediately
await pageClient.FireCommandAsync(Domains.Runtime.Evaluate("alert('Hello there')"));
```

### Listen for events
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

### Generate models

TBD
