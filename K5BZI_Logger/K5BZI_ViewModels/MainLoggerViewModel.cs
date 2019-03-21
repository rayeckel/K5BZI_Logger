using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_Models.Main;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System;
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

        public void SelectEvent(LogListing selectedLog)
        {
            Model.MainVisibility = Visibility.Visible;
        }

        public void CreateNewLog(Event newEvent)
        {
            CreateNewLogEntry();

            Model.LogEntry.Event.EventName = newEvent.EventName;
        }

        private void Initialize()
        {
            Model = new MainModel
            {
                CreateNewEntryAction = () => CreateMockLogEntry(),
                LogItAction = () => SaveLogEntry()
            };

            CreateMockLogEntry();
        }

        private void SaveLogEntry()
        {
            Model.LogEntries.Add(Model.LogEntry.Clone());

            var eventName = Model.LogEntry.Event.EventName.Replace(" ", "_");
            var fileName = String.Format("{0}_{1}", eventName, DateTime.UtcNow.ToString("yyyy'-'MM'-'dd"));

            _fileStoreService.WriteToFile(Model.LogEntries, fileName);

            CreateNewLogEntry();
        }

        private void CreateMockLogEntry()
        {
            CreateNewLogEntry();
            Model.LogEntry.CallSign = "KC5IHO";
            Model.LogEntry.ContactTime = DateTime.Now;
            Model.LogEntry.Event.EventName = "Parks On The Air";
            Model.LogEntry.Signal.Band = "40m";
            Model.LogEntry.Signal.Frequency = 7.225;
            Model.LogEntry.SignalReport.Sent = 599;
            Model.LogEntry.SignalReport.Received = 479;
        }

        private void CreateNewLogEntry()
        {
            Model.LogEntry.ClearProperties();
        }
    }
}
