using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using K5BZI_Services.Interfaces;
using Ninja.WebSockets;

namespace K5BZI_Services.Services
{
    public class NetworkService : INetworkService
    {
        private const int hostPort = 29131;

        public async void StartNetworkServer(string hostName)
        {
            /*
            var factory = new WebSocketClientFactory();
            var webSocket = await factory.ConnectAsync(new Uri("wss://example.com"));
            */

            //As a server, use the WebSocketServerFactory

            var factory = new WebSocketServerFactory();
            var tcpClient = new TcpClient(hostName, hostPort);

            var stream = tcpClient.GetStream();
            var context = await factory.ReadHttpHeaderFromStreamAsync(stream);

            if (context.IsWebSocketRequest)
            {
                var webSocket = await factory.AcceptWebSocketAsync(context);

                var result1 = Receive(webSocket);
            }
        }

        private async Task Send(WebSocket webSocket)
        {
            var array = Encoding.UTF8.GetBytes("Hello World");
            var buffer = new ArraySegment<byte>(array);

            await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        //Client and Server send and receive data the same way.

        //Receive data in an infinite loop until we receive a close frame from the server.
        //Receiving Data:
        private async Task Receive(WebSocket webSocket)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

                switch (result.MessageType)
                {
                    case WebSocketMessageType.Close:
                        return;
                    case WebSocketMessageType.Text:
                    case WebSocketMessageType.Binary:
                        string value = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                        Console.WriteLine(value);
                        break;
                }
            }
        }

        //Simple client Request / Response: The best approach to communicating 
        //using a web socket is to send and receive data on different worker threads as shown below.
        public async Task Run()
        {
            var factory = new WebSocketClientFactory();
            var uri = new Uri("ws://localhost:27416/chat");

            using (var webSocket = await factory.ConnectAsync(uri))
            {
                // receive loop
                var readTask = Receive(webSocket);

                // send a message
                await Send(webSocket);

                // initiate the close handshake
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);

                // wait for server to respond with a close frame
                await readTask;
            }
        }
    }
}
