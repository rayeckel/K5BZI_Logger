using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
        }

        public async Task FindHostsAsync(
            HostData networkData,
            ObservableCollection<HostData> availableAddresses,
            CancellationToken token)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var range = new IPAddressRange(networkData.GetNetworkAddress(), networkData.GetBroadcastAddress());

                foreach (var ip in range)
                {
                    if (token.IsCancellationRequested)
                        break;

                    if (ip.Equals(networkData))
                        continue;

                    var connection = new TcpClient();

                    try
                    {
                        await connection.ConnectAsync(ip.ToString(), hostPort)
                            .WithTimeout(TimeSpan.FromMilliseconds(100))
                            .ContinueWith((_) =>
                            {
                                if (_.Status != TaskStatus.Faulted)
                                    Application.Current.Dispatcher
                                        .BeginInvoke(new Action(() => availableAddresses.Add(new HostData("", ip.GetAddressBytes()))));
                            });
                    }
                    catch (Exception ex)
                    {
                    }

                    connection.Dispose();
                }
            }
        }

        public async Task SendTextMessageAsync(
            //string hostName,
            HostData networkData,
            string message)
        {
            //var ipHostInfo = await Dns.GetHostEntryAsync(hostName);
            //var ipAddress = ipHostInfo.AddressList[0];
            var ipEndPoint = new IPEndPoint(networkData, hostPort);

            using var client = new TcpClient();

            await client.ConnectAsync(ipEndPoint);

            var stream = client.GetStream();

            var ba = new ASCIIEncoding().GetBytes(message);

            stream.Write(ba, 0, ba.Length);

            var bb = new byte[100];

            var k = stream.Read(bb, 0, 100);

            for (int i = 0; i < k; i++)
                Console.Write(Convert.ToChar(bb[i]));

            client.Close();
        }

        public async Task<IPAddress> GetIpAddresAsync()
        {
            var ipAddress = default(IPAddress);

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var ipHostInfo = await Dns.GetHostEntryAsync(Environment.MachineName);

                ipAddress = ipHostInfo.AddressList
                    .FirstOrDefault(_ => _.AddressFamily != AddressFamily.InterNetworkV6 && _.IsPrivate());
            }

            return ipAddress;
        }

        public void StartServer(HostData networkData)
        {
            var listener = new TcpListener(networkData, hostPort);

            listener.Start();

            listener.BeginAcceptTcpClient(OnClientConnecting, listener);
        }

        private void OnClientConnecting(IAsyncResult ar)
        {
            try
            {
                if (ar.AsyncState is null)
                    throw new Exception("AsyncState is null. Pass it as an argument to BeginAcceptSocket method");

                // Get the server. This was passed as an argument to BeginAcceptSocket method
                var s = (TcpListener)ar.AsyncState;

                // listen for more clients. Note its callback is this same method (recusive call)
                s.BeginAcceptTcpClient(OnClientConnecting, s);

                // Get the client that is connecting to this server
                using var client = s.EndAcceptTcpClient(ar);

                // read data sent to this server by client that just connected
                var buffer = new byte[1024];
                var i = client.Client.Receive(buffer);

                // reply back the same data that was received to the client
                var k = client.Client.Send(buffer, 0, i, SocketFlags.None);

                Debug.WriteLine(Encoding.Default.GetString(buffer));

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
