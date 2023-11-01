using System.Threading.Tasks;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class SubmitViewModel : ISubmitViewModel
    {
        #region Properties

        private readonly ISubmitService _submitService;
        private readonly ILogViewModel _logViewModel;
        public SubmitModel SubmitModel { get; private set; }

        #endregion

        #region Constructors

        public SubmitViewModel(
            ISubmitService submitService,
            ILogViewModel logViewModel)
        {
            _submitService = submitService;
            _logViewModel = logViewModel;

            Initialize();
        }

        #endregion

        #region private Methods

        private void Initialize()
        {
            SubmitModel = new SubmitModel();

            _logViewModel.LogModel.SubmitLogAction = (_) => SubmitLogAsync();
        }

        private async Task SubmitLogAsync()
        {
            SubmitModel.IsOpen = true;
        }

        #endregion
    }
}
