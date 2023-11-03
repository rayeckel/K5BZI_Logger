using System.Threading.Tasks;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class DefaultsViewModel : IDefaultsViewModel
    {
        #region Properties

        public DefaultsModel Model { get; private set; }
        private readonly IDefaultsService _defaultsService;

        #endregion

        #region Constructors

        public DefaultsViewModel(IDefaultsService defaultsService)
        {
            _defaultsService = defaultsService;

            Initialize();
        }

        #endregion

        #region Private Methods

        private async Task Initialize()
        {
            Model = new DefaultsModel
            {
                EditDefaultsAction = (_) => EditDefaults(),
                UpdateDefaultsAction = (_) => UpdateDefaults(),
                Defaults = await _defaultsService.GetDefaultsAsync()
            };
        }

        private void EditDefaults()
        {
            Model.ShowCloseButton = true;
            Model.IsOpen = true;
        }

        private void UpdateDefaults()
        {
            Model.IsOpen = false;
            _defaultsService.UpdateDefaults(Model.Defaults);
        }

        #endregion
    }
}
