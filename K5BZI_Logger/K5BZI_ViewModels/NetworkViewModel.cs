using System.Threading.Tasks;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class NetworkViewModel : INetworkViewModel
    {
        #region Properties

        private readonly INetworkService _networkService;

        public NetworkModel NetworkModel { get; private set; }

        #endregion

        #region Constructors

        public NetworkViewModel(INetworkService networkService)
        {
            _networkService = networkService;

            Initialize();

        }

        #endregion

        #region Private Methods

        private async Task Initialize()
        {
            NetworkModel = new NetworkModel
            {
                SendMessageAction = async (_) => await SendMessageAsync(),
            };

            await SetupNetwork();
        }

        private async Task SetupNetwork()
        {
            //Task.Run(async () => await _networkService.StartServerAsync());
            Task.Run(async () => await _networkService.FindHostsAsync(NetworkModel.NetworkAddresses));
        }

        private async Task SendMessageAsync()
        {
            await _networkService.SendTextMessageAsync(NetworkModel.ActiveAddress, "Hello World");
        }

        #endregion
    }
}
