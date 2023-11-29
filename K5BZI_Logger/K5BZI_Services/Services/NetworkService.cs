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
            Task.Run(async () => await StartServerAsync());
            Task.Run(async () => await FindHostsAsync());
        }

        public async Task FindHostsAsync()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var ipHostInfo = await Dns.GetHostEntryAsync(Environment.MachineName);
                var availableAddresses = new NetworkAddressCollection();

                foreach (var ipAddress in ipHostInfo.AddressList
                    .Where(_ => _.AddressFamily != AddressFamily.InterNetworkV6 && _.IsPrivate()))
                {
                    var range = new IPAddressRange(ipAddress.GetNetworkAddress(), ipAddress.GetBroadcastAddress());

                    foreach (var ip in range)
                    {
                        using var connection = new TcpClient();

                        try
                        {
                            connection.ConnectAsync(ip.ToString(), hostPort).WithTimeout(TimeSpan.FromMilliseconds(200));
                            availableAddresses.Add(ip);
                        }
                        catch (Exception ex)
                        {
                            connection.Dispose();
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

                var listener = new TcpListener(ipAddress, hostPort);
                listener.Start();

                listener.BeginAcceptTcpClient(ThreadProc, listener);
            }

            return String.Empty;
        }
        private void ThreadProc(IAsyncResult ar)
        {
            //var client = (TcpClient)obj;
            // Do your work here
        }
    }
}
