using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows.Forms;

namespace K5BZI_ViewModels
{
    public class SelectEventViewModel : ISelectEventViewModel
    {
        #region Properties

        public SelectEventModel Model { get; private set; }
        private IEventService _eventService;
        private readonly IFileStoreService _fileStoreService;
        private readonly IMainLoggerViewModel _mainLoggerViewModel;

        #endregion

        #region Constructors

        public SelectEventViewModel()
        {
            _fileStoreService = ServiceLocator.Current.GetInstance<IFileStoreService>();
            _mainLoggerViewModel = ServiceLocator.Current.GetInstance<IMainLoggerViewModel>();

            Initialize();
            GetExistingLogs();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            Model = new SelectEventModel
            {
                CreateNewLogAction = () => CreateNewLog(),
                SelectLogAction = () => SelectLog()
            };
        }

        private void GetExistingLogs()
        {
            _fileStoreService
                .GetLogListing()
                .ForEach(log =>
            {
                Model.ExistingLogs.Add(log);
            });
        }

        private void CreateNewLog()
        {
            if (String.IsNullOrEmpty(Model.EventName))
            {
                MessageBox.Show("Please provide the Event Name.");
                return;
            }

            _eventService = ServiceLocator.Current.GetInstance<IEventService>();

            var newEvent = _eventService.CreateNewEvent(Model.EventName);
            _mainLoggerViewModel.CreateNewLog(newEvent);

            Model.IsOpen = false;
        }

        private void SelectLog()
        {
            _mainLoggerViewModel.SelectEvent(Model.SelectedLog);

            Model.IsOpen = false;
        }

        #endregion
    }
}
