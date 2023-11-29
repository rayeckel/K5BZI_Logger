using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace K5BZI_Services.Interfaces
{
    public interface INetworkService
    {
        Task FindHostsAsync(List<IPAddress> availableAddresses);

        Task SendTextMessageAsync(
            string hostName,
            string message = "Hi friends 👋!<|EOM|>");

        Task<string> StartServerAsync();
    }
}
