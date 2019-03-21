using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Forms;

namespace K5BZI_ViewModels
{
    public class SelectEventViewModel : ISelectEventViewModel
    {
        public SelectEventModel Model { get; private set; }

        private IEventService _eventService;
        private readonly IFileStoreService _fileStoreService;
        private readonly IMainLoggerViewModel _mainLoggerViewModel;

        public SelectEventViewModel()
        {
            _fileStoreService = ServiceLocator.Current.GetInstance<IFileStoreService>();
            _mainLoggerViewModel = ServiceLocator.Current.GetInstance<IMainLoggerViewModel>();

            Initialize();
            GetExistingLogs();
        }

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
            MessageBox.Show("Not implemented");
            return;

            _eventService = ServiceLocator.Current.GetInstance<IEventService>();

            var newEvent = _eventService.CreateNewEvent("Foo");
            _mainLoggerViewModel.CreateNewLog(newEvent);
        }

        private void SelectLog()
        {
            _mainLoggerViewModel.SelectEvent(Model.SelectedLog);
        }
    }
}
