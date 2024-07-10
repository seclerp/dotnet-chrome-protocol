namespace ChromeProtocol.Tools.Tests

open System.Text.Json
open ChromeProtocol.Tools.Logging

type XunitErrorFormatter() =
  interface IErrorFormatter with
    member this.Format(fileName, error) = $"{fileName}: {JsonSerializer.Serialize(error)}"
    member this.Format(error) = JsonSerializer.Serialize(error)

