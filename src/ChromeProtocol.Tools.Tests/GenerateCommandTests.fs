namespace Chrome.DevTools.Protocol.Tools.Tests

open System
open System.IO
open JetBrains.Wasm.Debugger.ChromeProtocol.Tests.Extensions.Logging
open JetBrains.Wasm.Debugger.ChromeProtocol.Tools.Commands
open Xunit
open Xunit.Abstractions

type GenerateCommandTests(outputHelper: ITestOutputHelper) =
  let testFolderPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() |> string)
  do Directory.CreateDirectory(testFolderPath) |> ignore
  do outputHelper.WriteLine(testFolderPath)

  [<Theory>]
  [<InlineData("MonoProtocol")>]
  member _.``Valid definition should be generated correctly`` (folder) =
    async {
      let expectedContents =
        Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Data", folder), "*.cs")
        |> Array.map(fun path ->
          File.ReadAllLines(path)
          |> Array.map(fun str -> str.Trim())
          |> String.concat Environment.NewLine
          |> (fun contents -> (Path.GetFileName path, contents)))
        |> Array.sortBy fst

      use logger = new XunitLogger(outputHelper)
      let generateCommand = GenerateCommand(logger, XunitErrorFormatter())
      let settings = GenerateCommand.Settings()
      settings.Clean <- false
      settings.InputFiles <- [| Path.Combine(Directory.GetCurrentDirectory(), "Data", folder, "definition.json") |]
      settings.OutputFolder <- testFolderPath
      settings.Namespace <- "Protocol.Generated"

      let! code = generateCommand.ExecuteAsync(null, settings) |> Async.AwaitTask

      let actualContents =
        Directory.GetFiles(testFolderPath, "*.cs")
        |> Array.map(fun path ->
          File.ReadAllLines(path)
          |> Array.map(fun str -> str.Trim())
          |> String.concat Environment.NewLine
          |> (fun contents -> (Path.GetFileName path, contents)))
        |> Array.sortBy fst

      let pairs = Array.zip expectedContents actualContents
      for ((expectedFileName, expectedContents), (actualFileName, actualContents)) in pairs do
        Assert.Equal(expectedFileName, actualFileName)
        Assert.Equal(expectedContents, actualContents)
      Assert.Equal(0, code)
    } |> Async.StartAsTask

  interface IDisposable with
    member this.Dispose () =
      if Directory.Exists testFolderPath then
        Directory.Delete(testFolderPath, true)

