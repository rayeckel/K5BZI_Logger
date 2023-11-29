using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;

namespace K5BZI_Services.Interfaces
{
    public interface INetworkService
    {
        Task FindHostsAsync(ObservableCollection<IPAddress> availableAddresses);

        Task SendTextMessageAsync(
            IPAddress ipAddress,
            string message = "Hi friends 👋!<|EOM|>");

        Task<string> StartServerAsync();
    }
}
