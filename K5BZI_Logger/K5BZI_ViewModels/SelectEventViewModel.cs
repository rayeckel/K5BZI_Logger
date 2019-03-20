using System;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class SelectEventViewModel : ISelectEventViewModel
    {
        public SelectEventModel Model { get; private set; }

        private readonly IFileStoreService _fileStoreService;

        public SelectEventViewModel(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;

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
                .ForEach(_ =>
            {
                //Model.ExistingLogs.Add();
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
