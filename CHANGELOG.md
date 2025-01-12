# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.1.0]

### Added

- `ChromeProtocol.Runtime.Browser.ChromiumLauncher` class that simplifies starting Chrome and obtaining debugging endpoint
- Simple example project `ChromeProtocol.Usage` that highlights usage of the `ChromeProtocol.Runtime` and `ChromeProtocol.Domains` libraries

### Removed

- `ChromeProtocol.Runtime.Messaging.Logger.ChannelProtocolClientLogger`

### Changed

- `ProtocolClientLogger.LogConnected` became `protected`
- `ProtocolClientLogger.LogDisconnected` became `protected`
- `ProtocolClientLogger.LogOutgoingRequest` became `protected`
- `ProtocolClientLogger.LogIncomingResponse` became `protected`
- `ProtocolClientLogger.LogIncomingUnknownResponse` became `protected`
- `ProtocolClientLogger.LogIncomingError` became `protected`
- `ProtocolClientLogger.LogIncomingEvent` became `protected`
- `ChromeProtocol.Core.Extensions.ReflectionExtensions` became `internal`
- `System.Text.Json` has been updated to the version `8.0.5` to address high-severity vulnerabilities

### Fixed

- Different typos and confusing `[Obsolete]` attributes
- Multiple nullability issues in different places across packages

## [2.0.1]

### Changed

- Bump Domains version to Chrome base revision 1367902
- Reflect revision in the Domains package version

## [2.0.0]

### Changed

- Replace `Netwonsoft.Json` with `System.Text.Json` (#6 by @y0ung3r)

## [1.3.0]

### Added

- New interfaces to represent weakly-typed arrays and objects - `IObjectType` and `IArrayType` with corresponding JSON converters

### Fixed

- Fix code generation problem with `object` and `array` type definitions without properties (#7)

## [1.2.3]

### Fixed

- Fix binary inconsistency between different origins of the packages in different package sources

## [1.2.2]

### Fixed

- Fix missing copyright info in the NuGet packages metadata

## [1.2.1]

### Fixed

- Fix missing handling of invalid socket state while sending WebSocket requests 
- Fix missing disconnect event invocation

## [1.2.0]

### Added

- Add `SessionId` parameter for `SubscribeAsync` and `SubscribeSync` methods of `ProtocolClient`

### Fixed

- Fix Subscribe methods replicating events for all connected sessions

## [1.1.0]

### Removed

- Remove preview version postfix

## [1.1.0-preview2]

### Fixed

- Fix `dotnet cdp` tools packaging

## [1.1.0-preview1]

### Added

- Initial release

[Unreleased]: https://github.com/seclerp/dotnet-chrome-protocol/compare/2.1.0...HEAD
[2.1.0]: https://github.com/seclerp/dotnet-chrome-protocol/compare/2.0.1...2.1.0
[2.0.1]: https://github.com/seclerp/dotnet-chrome-protocol/compare/2.0.0...2.0.1
[2.0.0]: https://github.com/seclerp/dotnet-chrome-protocol/compare/1.3.0...2.0.0
[1.3.0]: https://github.com/seclerp/dotnet-chrome-protocol/compare/1.2.3...1.3.0
[1.2.3]: https://github.com/seclerp/dotnet-chrome-protocol/compare/1.2.2...1.2.3
[1.2.2]: https://github.com/seclerp/dotnet-chrome-protocol/compare/1.2.1...1.2.2
[1.2.1]: https://github.com/seclerp/dotnet-chrome-protocol/compare/1.2.0...1.2.1
[1.2.0]: https://github.com/seclerp/dotnet-chrome-protocol/compare/1.1.0...1.2.0
[1.1.0]: https://github.com/seclerp/dotnet-chrome-protocol/compare/1.1.0-preview2...1.1.0
[1.1.0-preview2]: https://github.com/seclerp/dotnet-chrome-protocol/compare/1.1.0-preview1...1.1.0-preview2
[1.1.0-preview1]: https://github.com/seclerp/dotnet-chrome-protocol/releases/tag/v1.1.0-preview1
