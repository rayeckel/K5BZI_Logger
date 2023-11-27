using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using K5BZI_Models.Base;
using K5BZI_Models.Extensions;
using K5BZI_Services.Interfaces;
using NetTools;

namespace K5BZI_Services.Services
{
    public class NetworkService : INetworkService
    {
        private const int hostPort = 15249;
        private const string eom = "<|EOM|>";
        private const string ackMessage = "<|ACK|>";

        public async Task FindHostsAsync()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var ipHostInfo = await Dns.GetHostEntryAsync(Environment.MachineName);
                var availableAddresses = new NetworkAddressCollection();

                using var client = new TcpClient();

                var success = client.ConnectAsync("192.168.1.250", hostPort).Wait(1000);

                foreach (var ipAddress in ipHostInfo.AddressList
                    .Where(_ => _.AddressFamily != AddressFamily.InterNetworkV6 && _.IsPrivate()))
                {
                    var range = new IPAddressRange(ipAddress.GetNetworkAddress(), ipAddress.GetBroadcastAddress());

                    foreach (var ip in range)
                    {
                        try
                        {
                            if (client.ConnectAsync(ip.ToString(), hostPort).Wait(1000))
                            {
                                availableAddresses.Add(ip);
                            }
                        }
                        catch (SocketException ex)
                        {
                            continue;
                        }
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

                var ipEndPoint = new IPEndPoint(ipAddress, hostPort);

                using Socket listener = new(
                    ipEndPoint.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp);

                listener.Bind(ipEndPoint);
                listener.Listen(100);

                var handler = await listener.AcceptAsync();

                while (true)
                {
                    var buffer = new byte[1_024];
                    var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                    var response = Encoding.UTF8.GetString(buffer, 0, received);

                    if (response.IndexOf(eom) > -1 /* is end of message */)
                    {
                        var echoBytes = Encoding.UTF8.GetBytes(ackMessage);

                        await handler.SendAsync(echoBytes, 0);

                        break;
                    }

                    return response;
                }
            }

            Console.WriteLine("Network not available");
            return string.Empty;
        }
    }
}
