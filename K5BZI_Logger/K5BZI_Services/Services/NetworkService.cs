using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using K5BZI_Services.Interfaces;

namespace K5BZI_Services.Services
{
    public class NetworkService : INetworkService
    {
        private const int hostPort = 15249;

        public void StartListener()
        {
            var port = 13000;
            var localAddr = IPAddress.Parse("127.0.0.1");
            var server = new TcpListener(localAddr, port);

            try
            {
                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                var bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    using TcpClient client = server.AcceptTcpClient();

                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.
                        data = data.ToUpper();

                        var msg = Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        public async void StartNetworkServer(string hostName)
        {
            //As a server, use the WebSocketServerFactory
            //var factory = new WebSocketServerFactory();
            /*
            var factory = new WebSocketClientFactory();
            var webSocket = await factory.ConnectAsync(new Uri("wss://example.com"));

            var tcpClient = new TcpClient(hostName, hostPort);

            var stream = tcpClient.GetStream();
            var context = await factory.ReadHttpHeaderFromStreamAsync(stream);

            if (context.IsWebSocketRequest)
            {
                var webSocket = await factory.AcceptWebSocketAsync(context);

                var result1 = Receive(webSocket);
            }
            */
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
        /*
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
        */
    }
}
