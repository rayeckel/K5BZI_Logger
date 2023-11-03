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
        private readonly IOperatorViewModel _operatorViewModel;

        public SubmitModel SubmitModel { get; private set; }

        #endregion

        #region Constructors

        public SubmitViewModel(
            ISubmitService submitService,
            IOperatorViewModel operatorViewModel)
        {
            _submitService = submitService;
            _operatorViewModel = operatorViewModel;

            Initialize();
        }

        #endregion

        #region private Methods

        private void Initialize()
        {
            SubmitModel = new SubmitModel
            {
                SelectedSubmitOperator = _operatorViewModel.OperatorModel.ActiveOperator,
                SubmitLogAction = async (_) => await SubmitLogAsync()
            };
        }

        private async Task SubmitLogAsync()
        {
            SubmitModel.IsOpen = true;
        }

        #endregion
    }
}
