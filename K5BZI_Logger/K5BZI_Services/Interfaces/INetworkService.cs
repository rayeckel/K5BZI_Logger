using System.Collections.ObjectModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using K5BZI_Models.Base;

namespace K5BZI_Services.Interfaces
{
    public interface INetworkService
    {
        Task<IPAddress> GetIpAddresAsync();

        void StartServer(HostData networkData);

        Task FindHostsAsync(
            HostData networkData,
            ObservableCollection<HostData> availableAddresses,
            CancellationToken token);

        Task SendTextMessageAsync(
            HostData networkData,
            string message = "Hi friends 👋!<|EOM|>");
    }
}
