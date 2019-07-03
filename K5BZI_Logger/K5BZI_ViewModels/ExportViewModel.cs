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
        private readonly IMainLoggerViewModel _mainLoggerViewModel;

        #endregion

        #region Constructors

        public ExportViewModel(
            IExportService exportService,
            IMainLoggerViewModel mainLoggerViewModel)
        {
            _exportService = exportService;
            _mainLoggerViewModel = mainLoggerViewModel;

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

            _mainLoggerViewModel.Model.SelectExportLogAction = (_) => SelectLog();
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
                _mainLoggerViewModel.Model.Event,
                _mainLoggerViewModel.Model.LogEntries,
                Model.SelectedExport);
        }

        #endregion
    }
}
