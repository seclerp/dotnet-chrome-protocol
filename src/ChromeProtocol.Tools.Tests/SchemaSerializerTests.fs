namespace Chrome.DevTools.Protocol.Tools.Tests

open System.IO
open System.Text
open JetBrains.Wasm.Debugger.ChromeProtocol.Tools.Schema
open Xunit

type SchemaSerializerTests () =
  [<Fact>]
  member _.``Parse definition, should succeed``() =
    let definition = """
    {
      "version": {
        "major": "7",
        "minor": "0"
      },
      "domains": [
        {
          "domain": "Mono",
          "description": "Contains commands and events related to Mono runtime.",
          "experimental": true,
          "events": [
            {
              "name": "runtimeReady",
              "description": "Issued when Mono runtime is ready to use."
            }
          ]
        },
        {
          "domain": "DotnetDebugger",
          "description": "TBD",
          "experimental": true,
          "commands": [
            {
              "name": "setDebuggerProperty",
              "description": "TBD.",
              "availableSince": "7",
              "parameters": [
                {
                  "name": "JustMyCodeStepping",
                  "description": "If true, enables stepping only into user code, not library one.",
                  "type": "boolean"
                }
              ],
              "returns": [
                {
                  "name": "justMyCodeEnabled",
                  "description": "TBD",
                  "optional": true,
                  "type": "boolean"
                }
              ]
            },
            {
              "name": "setNextIP",
              "description": "TBD",
              "availableSince": "7",
              "parameters": [
                {
                  "name": "location",
                  "description": "TBD",
                  "$ref": "DotnetDebugger.SourceLocation"
                }
              ]
            },
            {
              "name": "applyUpdates",
              "description": "TBD",
              "availableSince": "7",
              "parameters": [
                {
                  "name": "moduleGUID",
                  "description": "TBD",
                  "type": "string"
                },
                {
                  "name": "dmeta",
                  "description": "The metadata changes to be applied",
                  "type": "string"
                },
                {
                  "name": "dil",
                  "description": "The IL changes to be applied",
                  "type": "string"
                },
                {
                  "name": "dpdb",
                  "description": "The PDB changes to be applied",
                  "type": "string"
                }
              ],
              "returns": []
            },
            {
              "name": "addSymbolServerUrl",
              "description": "Note: this command will never respond, so don't wait for the response. When succeeded, it will emit 'Debugger.scriptParsed' event per each parsed .NET source file.",
              "availableSince": "5",
              "parameters": [
                {
                  "name": "url",
                  "description": "TBD",
                  "type": "string"
                }
              ]
            },
            {
              "name": "getMethodLocation",
              "description": "TBD",
              "parameters": [
                {
                  "name": "assemblyName",
                  "description": "TBD",
                  "type": "string"
                },
                {
                  "name": "typeName",
                  "description": "TBD",
                  "type": "string"
                },
                {
                  "name": "methodName",
                  "description": "TBD",
                  "type": "string"
                }
              ],
              "returns": [
                {
                  "name": "line",
                  "description": "TBD",
                  "type": "integer"
                },
                {
                  "name": "column",
                  "description": "TBD",
                  "type": "integer"
                },
                {
                  "name": "url",
                  "description": "TBD",
                  "type": "string"
                }
              ]
            }
          ],
          "events": []
        },
        {
          "domain": "Runtime",
          "description": "TBD",
          "experimental": true,
          "types": [],
          "commands": [
            {
              "name": "callFunctionOn",
              "description": "TBD",
              "parameters": [
                {
                  "name": "objectId",
                  "description": "TBD",
                  "type": "string"
                },
                {
                  "name": "typeName",
                  "description": "TBD",
                  "type": "string"
                },
                {
                  "name": "methodName",
                  "description": "TBD",
                  "type": "string"
                }
              ],
              "returns": [
                {
                  "name": "result",
                  "description": "TBD",
                  "type": "object"
                }
              ]
            }
          ],
          "events": []
        }
      ]
    }
    """

    use stream = new MemoryStream(Encoding.UTF8.GetBytes definition)
    let result = SchemaSerializer.ParseDefinition stream

    Assert.NotNull result
