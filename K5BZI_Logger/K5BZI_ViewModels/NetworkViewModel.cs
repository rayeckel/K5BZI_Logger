﻿using System.Threading.Tasks;
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

        private void Initialize()
        {
            NetworkModel = new NetworkModel
            {
                SendMessageAction = async (_) => await SendMessageAsync(),
            };
        }

        private async Task SendMessageAsync()
        {
            await _networkService.SendTextMessageAsync("RadioPC", "Hello World");
        }

        #endregion
    }
}
