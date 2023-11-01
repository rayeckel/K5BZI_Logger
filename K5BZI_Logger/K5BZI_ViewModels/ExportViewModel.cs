using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class ExportViewModel : IExportViewModel
    {
        #region Properties

        public SelectExportModel Model { get; private set; }
        private readonly IExportService _exportService;
        private readonly IEventViewModel _eventViewModel;
        private readonly ILogViewModel _logViewModel;

        #endregion

        #region Constructors

        public ExportViewModel(
            IExportService exportService,
            IEventViewModel eventViewModel,
            ILogViewModel logViewModel)
        {
            _exportService = exportService;
            _eventViewModel = eventViewModel;
            _logViewModel = logViewModel;

            Initialize();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            Model = new SelectExportModel
            {
                SelectExportAction = (_) => ChangeExport()
            };

            _logViewModel.LogModel.SelectExportLogAction = (_) => SelectLog();
        }

        public void SelectLog()
        {
            Model.ShowCloseButton = true;
            Model.IsOpen = true;
        }

        private void ChangeExport()
        {
            Model.IsOpen = false;

            _exportService.ExportLog(
                _eventViewModel.EventModel.Event,
                _logViewModel.LogModel.LogEntries,
                Model.SelectedExport);
        }

        #endregion
    }
}
