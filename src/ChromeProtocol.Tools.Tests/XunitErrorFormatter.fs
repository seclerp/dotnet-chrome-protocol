namespace Chrome.DevTools.Protocol.Tools.Tests

open ChromeProtocol.Tools.Logging
open Newtonsoft.Json

type XunitErrorFormatter() =
  interface IErrorFormatter with
    member this.Format(fileName, error) = $"{fileName}: {JsonConvert.SerializeObject(error)}"
    member this.Format(error) = JsonConvert.SerializeObject(error)

