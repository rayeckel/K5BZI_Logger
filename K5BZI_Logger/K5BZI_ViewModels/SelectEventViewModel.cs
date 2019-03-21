using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace K5BZI_ViewModels
{
    public class SelectEventViewModel : ISelectEventViewModel
    {
        public SelectEventModel Model { get; private set; }

        private readonly IFileStoreService _fileStoreService;

        public SelectEventViewModel()
        {
            _fileStoreService = ServiceLocator.Current.GetInstance<IFileStoreService>();

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

        }

        private void SelectLog()
        {

        }
    }
}
