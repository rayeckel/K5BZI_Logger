using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_Models.Main;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace K5BZI_ViewModels
{
    public class MainLoggerViewModel : IMainLoggerViewModel
    {
        #region Properties

        public MainModel Model { get; private set; }
        private string _fileName;
        private readonly IFileStoreService _fileStoreService;

        #endregion

        #region Constructors

        public MainLoggerViewModel(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;

            Initialize();
        }

        #endregion

        #region Public Methods

        public async void SelectEvent(LogListing selectedLog)
        {
            _fileName = selectedLog.FileName;
            var logEntries = await _fileStoreService.ReadLog(_fileName);

            Model.EventName = logEntries.First().Event.EventName;

            Model.LogEntries.Clear();

            if (logEntries.Any())
            {
                logEntries.ForEach(_ => Model.LogEntries.Add(_));

                Model.LogEntry.Signal.Band = logEntries.Last().Signal.Band;
                Model.LogEntry.Signal.Frequency = logEntries.Last().Signal.Frequency;
            }
        }

        public void CreateNewLog(Event newEvent)
        {
            var eventName = newEvent.EventName.Replace(" ", "_");
            _fileName = String.Format("{0}_{1}", eventName, DateTime.UtcNow.ToString("yyyy'-'MM'-'dd"));

            CreateNewLogEntry();

            Model.LogEntries.Clear();
            Model.LogEntry.Event.EventName = Model.EventName = newEvent.EventName;
        }

        #endregion

        #region Private Methods

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
            if (String.IsNullOrEmpty(Model.LogEntry.CallSign))
            {
                MessageBox.Show("You must provide a valid Call Sign.", "Oops!");
                return;
            }

            Model.LogEntries.Add(Model.LogEntry.Clone());

            _fileStoreService.WriteToFile(Model.LogEntries, _fileName);

            CreateNewLogEntry();
        }

        private void CreateNewLogEntry()
        {
            Model.LogEntry.ClearProperties();
        }

        #endregion
    }
}
