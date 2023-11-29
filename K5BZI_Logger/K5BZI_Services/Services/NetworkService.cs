using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using K5BZI_Models.Extensions;
using K5BZI_Services.Interfaces;
using Microsoft.VisualStudio.Threading;
using NetTools;

namespace K5BZI_Services.Services
{
    public class NetworkService : INetworkService
    {
        private const int hostPort = 15249;
        private const string eom = "<|EOM|>";
        private const string ackMessage = "<|ACK|>";

        public NetworkService()
        {
        }

        public async Task FindHostsAsync(List<IPAddress> availableAddresses)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var ipHostInfo = await Dns.GetHostEntryAsync(Environment.MachineName);

                foreach (var ipAddress in ipHostInfo.AddressList
                    .Where(_ => _.AddressFamily != AddressFamily.InterNetworkV6 && _.IsPrivate()))
                {
                    var range = new IPAddressRange(ipAddress.GetNetworkAddress(), ipAddress.GetBroadcastAddress());

                    foreach (var ip in range)
                    {
                        using var connection = new TcpClient();

                        try
                        {
                            await connection.ConnectAsync(ip.ToString(), hostPort)
                                .WithTimeout(TimeSpan.FromMilliseconds(200))
                                .ContinueWith((_) =>
                                {
                                    if (_.Status != TaskStatus.Faulted)
                                        availableAddresses.Add(ip);
                                });
                        }
                        catch (Exception ex)
                        {
                        }

                        connection.Dispose();
                    }
                }
            }
        }

        public async Task SendTextMessageAsync(
            string hostName,
            string message)
        {
            var ipHostInfo = await Dns.GetHostEntryAsync(hostName);
            var ipAddress = ipHostInfo.AddressList[0];
            var ipEndPoint = new IPEndPoint(ipAddress, hostPort);

            using Socket client = new(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            await client.ConnectAsync(ipEndPoint);

            while (true)
            {
                var messageBytes = Encoding.UTF8.GetBytes(message);

                await client.SendAsync(messageBytes, SocketFlags.None);

                // Receive ack.
                var buffer = new byte[1_024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);

                if (response == "<|ACK|>")
                {
                    Console.WriteLine(
                        $"Socket client received acknowledgment: \"{response}\"");
                    break;
                }
            }

            client.Shutdown(SocketShutdown.Both);
        }

        public async Task<string> StartServerAsync()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var ipHostInfo = await Dns.GetHostEntryAsync(Environment.MachineName);
                var ipAddress = ipHostInfo.AddressList
                    .FirstOrDefault(_ => _.AddressFamily != AddressFamily.InterNetworkV6 && _.IsPrivate());

                if (ipAddress == null)
                    return String.Empty;

                var listener = new TcpListener(ipAddress, hostPort);
                listener.Start();

                listener.BeginAcceptTcpClient(OnClientConnecting, listener);
            }

            return String.Empty;
        }
        private void OnClientConnecting(IAsyncResult ar)
        {
            try
            {
                Console.WriteLine("Client connecting...");

                if (ar.AsyncState is null)
                    throw new Exception("AsyncState is null. Pass it as an argument to BeginAcceptSocket method");

                // Get the server. This was passed as an argument to BeginAcceptSocket method
                TcpListener s = (TcpListener)ar.AsyncState;

                // listen for more clients. Note its callback is this same method (recusive call)
                s.BeginAcceptTcpClient(OnClientConnecting, s);

                // Get the client that is connecting to this server
                using TcpClient client = s.EndAcceptTcpClient(ar);

                Console.WriteLine("Client connected succesfully");

                // read data sent to this server by client that just connected
                byte[] buffer = new byte[1024];
                var i = client.Client.Receive(buffer);
                Console.WriteLine($"Received {i} bytes from client");

                // reply back the same data that was received to the client
                var k = client.Client.Send(buffer, 0, i, SocketFlags.None);
                Console.WriteLine($"Sent {k} bytes to slient as reply");

                // close the tcp connection
                client.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
