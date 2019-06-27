using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using K5BZI_Models;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using K5BZI_Models.Enums;

namespace K5BZI_ViewModels
{
    public class SelectExportViewModel : ISelectExportViewModel
    {
        #region Properties

        public SelectExportModel Model { get; private set; }

        #endregion

        #region Constructors

        public SelectExportViewModel()
        {
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

            var mainLoggerViewModel = ServiceLocator.Current.GetInstance<IMainLoggerViewModel>();
            mainLoggerViewModel.Model.SelectExportLogAction = () => SelectLog();
        }

        public void SelectLog()
        {
            Model.ShowCloseButton = true;
            Model.IsOpen = true;
        }

        private void ChangeExport()
        {
            Model.IsOpen = false;

            var mainLoggerViewModel = ServiceLocator.Current.GetInstance<IMainLoggerViewModel>();
            var exportService = ServiceLocator.Current.GetInstance<IExportService>();

            exportService.ExportLog(
                mainLoggerViewModel.Model.Event,
                mainLoggerViewModel.Model.LogEntries, 
               (LogType)Model.SelectedExport);
        }

        #endregion
    }
}
