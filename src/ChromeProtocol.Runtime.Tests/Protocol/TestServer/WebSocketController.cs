using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using ChromeProtocol.Runtime.Messaging;
using ChromeProtocol.Runtime.Messaging.Json;
using Microsoft.AspNetCore.Mvc;

namespace ChromeProtocol.Runtime.Tests.Protocol.TestServer;

[Route("")]
public class WebSocketController : ControllerBase
{
  [Route("/ws")]
  public async Task Get(CancellationToken token)
  {
    if (HttpContext.WebSockets.IsWebSocketRequest)
    {
      using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync().ConfigureAwait(false);
      await ProcessConnection(webSocket, token);
    }
    else
    {
      HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    }
  }

  static async Task ProcessConnection(WebSocket webSocket, CancellationToken token)
  {
    var buffer = new byte[1024 * 4];
    var receiveResult = await webSocket.ReceiveAsync(
      new ArraySegment<byte>(buffer), CancellationToken.None).ConfigureAwait(false);

    while (!receiveResult.CloseStatus.HasValue)
    {
      await ProcessMessage(webSocket, new ArraySegment<byte>(buffer, 0, receiveResult.Count)).ConfigureAwait(false);

      receiveResult = await webSocket.ReceiveAsync(
        new ArraySegment<byte>(buffer), token);
    }

    await webSocket.CloseAsync(
      receiveResult.CloseStatus.Value,
      receiveResult.CloseStatusDescription,
      token).ConfigureAwait(false);
  }

  static async Task ProcessMessage(WebSocket webSocket, ArraySegment<byte> incoming)
  {
    var message = JsonSerializer.Deserialize<ProtocolRequest<JsonObject>>(Encoding.UTF8.GetString(incoming.ToArray()), JsonProtocolSerialization.Settings)
                  ?? throw new NotImplementedException();
    switch (message.Method)
    {
      case "Echo":
      {
        var result = new EchoResult(message.Params["message"]?.Deserialize<string>() ?? throw new NotImplementedException());
        var response = new ProtocolResponse<EchoResult>(message.Id, result, null);
        await Send(webSocket, response).ConfigureAwait(false);
        break;
      }
      case "AlwaysError":
      {
        var response = new ProtocolResponse<object>(message.Id, null, new ProtocolErrorInfo(-1, null, "I'm always failing", null));
        await Send(webSocket, response).ConfigureAwait(false);
        break;
      }
      case "TriggerEvent":
      {
        var response = new ProtocolEvent<EventTriggeredEvent>("EventTriggered", new EventTriggeredEvent());
        await Send(webSocket, response).ConfigureAwait(false);
        break;
      }
    }
  }

  private static async Task Send(WebSocket webSocket, IProtocolMessage message)
  {
    var serialized = JsonSerializer.Serialize<object>(message, JsonProtocolSerialization.Settings);
    var bytes = Encoding.UTF8.GetBytes(serialized);

    await webSocket.SendAsync(
      new ArraySegment<byte>(bytes, 0, bytes.Length),
      WebSocketMessageType.Text,
      true,
      CancellationToken.None).ConfigureAwait(false);
  }
}
