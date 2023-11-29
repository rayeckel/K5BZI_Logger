using K5BZI_Models.Main;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class MainViewModel : IMainViewModel // NOT CURRE NTLY USED. (NOT IN DI)
    {
        #region Properties

        public MainModel Model { get; private set; }

        #endregion

        #region Constructors

        public MainViewModel(INetworkService networkService)
        {
            Initialize();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            Model = new MainModel();
        }

        #endregion
    }
}
