using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_Models.Main;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System;
using System.Linq;
using System.Windows;

namespace K5BZI_ViewModels
{
    public class MainLoggerViewModel : IMainLoggerViewModel
    {
        public MainModel Model { get; private set; }

        private readonly IFileStoreService _fileStoreService;

        public MainLoggerViewModel(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;

            Initialize();
        }

        public async void SelectEvent(LogListing selectedLog)
        {
            var logEntries = await _fileStoreService.ReadLog(selectedLog.FileName);

            Model.EventName = logEntries.First().Event.EventName;

            logEntries.ForEach(_ => Model.LogEntries.Add(_));

            Model.MainVisibility = Visibility.Visible;
        }

        public void CreateNewLog(Event newEvent)
        {
            CreateNewLogEntry();

            Model.LogEntry.Event.EventName = newEvent.EventName;

            Model.MainVisibility = Visibility.Visible;
        }

        private void Initialize()
        {
            Model = new MainModel
            {
                CreateNewEntryAction = () => CreateNewLogEntry(),
                LogItAction = () => SaveLogEntry()
            };
        }

        private void SaveLogEntry()
        {
            Model.LogEntries.Add(Model.LogEntry.Clone());

            var eventName = Model.LogEntry.Event.EventName.Replace(" ", "_");
            var fileName = String.Format("{0}_{1}", eventName, DateTime.UtcNow.ToString("yyyy'-'MM'-'dd"));

            _fileStoreService.WriteToFile(Model.LogEntries, fileName);

            CreateNewLogEntry();
        }

        private void CreateNewLogEntry()
        {
            Model.LogEntry.ClearProperties();
        }
    }
}
