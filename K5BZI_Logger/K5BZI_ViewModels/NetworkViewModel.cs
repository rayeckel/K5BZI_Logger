using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using K5BZI_Models.Base;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class NetworkViewModel : INetworkViewModel
    {
        #region Properties

        private CancellationTokenSource _cts;

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

        private void Initialize()
        {
            NetworkModel = new NetworkModel
            {
                SendMessageAction = async () => await SendMessageAsync(),
                CancelSearchAction = () => CancelSearch(),
                RescanAction = () => ScanNetwork(false),
                EditMessageAction = () => EditMessage()
            };

            ScanNetwork();
        }

        private async Task SetupNetworkAsync(bool startService)
        {
            if (NetworkModel.NetworkData == null)
            {
                var ipAddress = await _networkService.GetIpAddresAsync();

                if (ipAddress != default(IPAddress))
                {
                    NetworkModel.NetworkData = new HostData("", ipAddress.GetAddressBytes());
                }
            }

            if (NetworkModel.NetworkData != null)
            {
                if (startService)
                    _networkService.StartServer(NetworkModel.NetworkData);

                NetworkModel.RescanNetworkVisibility = Visibility.Collapsed;
                NetworkModel.SendMessageVisibility = Visibility.Collapsed;
                NetworkModel.SearchingNetworkVisibility = Visibility.Visible;

                await _networkService.FindHostsAsync(
                    NetworkModel.NetworkData,
                    NetworkModel.NetworkAddresses,
                    _cts.Token);

                NetworkModel.RescanNetworkVisibility = Visibility.Visible;
                NetworkModel.SendMessageVisibility = Visibility.Visible;
                NetworkModel.SearchingNetworkVisibility = Visibility.Collapsed;
            }
        }

        private async Task SendMessageAsync()
        {
            await _networkService.SendTextMessageAsync(NetworkModel.ActiveHost, NetworkModel.TextMessage.Message);
        }

        private void CancelSearch()
        {
            _cts.Cancel();

            NetworkModel.RescanNetworkVisibility = Visibility.Visible;
            NetworkModel.SendMessageVisibility = Visibility.Visible;
            NetworkModel.SearchingNetworkVisibility = Visibility.Collapsed;
        }

        private void ScanNetwork(bool startService = true)
        {
            NetworkModel.NetworkAddresses.Clear();

            _cts = new CancellationTokenSource();

            Task.Run(() => SetupNetworkAsync(startService));
        }

        private void EditMessage()
        {

        }

        #endregion
    }
}
