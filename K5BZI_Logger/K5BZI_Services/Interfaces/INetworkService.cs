using System.Threading.Tasks;

namespace K5BZI_Services.Interfaces
{
    public interface INetworkService
    {
        Task SendTextMessageAsync(
            string hostName,
            string message = "Hi friends 👋!<|EOM|>");

        Task<string> StartServerAsync();
    }
}
