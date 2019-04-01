using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_Models.Main;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace K5BZI_ViewModels
{
    public class MainLoggerViewModel : IMainLoggerViewModel
    {
        #region Properties

        public MainModel Model { get; private set; }
        private readonly ILogListingService _logListingService;

        #endregion

        #region Constructors

        public MainLoggerViewModel(ILogListingService logListingService)
        {
            _logListingService = logListingService;

            Initialize();

            var newDuplicate = new LogEntry { CallSign = "K5BZI" };
            newDuplicate.Signal.Band = "20M";
            Model.DuplicateEntries.Add(newDuplicate);
        }

        #endregion

        #region Public Methods

        public void SelectEvent(Event selectedEvent)
        {
            var logEntries = _logListingService.ReadLog(selectedEvent.LogFileName);

            Model.Event = selectedEvent;
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
            Model.Event = newEvent;
            Model.LogEntries.Clear();
            Model.LogEntry.ClearProperties();
            Model.LogEntry.EventId = newEvent.Id;
        }

        #endregion

        #region Private Methods

        private void CheckForDuplicates()
        {
            Model.DuplicateEntries.Clear();

            Model.LogEntries
                .Where(_ => _.CallSign.StartsWith(Model.LogEntry.CallSign))
                .ToList()
                .ForEach(_ => Model.DuplicateEntries.Add(_));
        }

        private void Initialize()
        {
            Model = new MainModel
            {
                CreateNewEntryAction = () => Model.LogEntry.ClearProperties(),
                LogItAction = () => SaveLogEntry(),
                ViewFileStoreAction = () => _logListingService.OpenLogListing()
            };

            Model.LogEntry.CheckDuplicateEntriesAction = () => CheckForDuplicates();
        }

        private void SaveLogEntry()
        {
            if (String.IsNullOrEmpty(Model.LogEntry.CallSign))
            {
                MessageBox.Show("You must provide a valid Call Sign.", "Oops!");
                return;
            }

            Model.LogEntries.Add(Model.LogEntry.Clone());

            _logListingService.SaveLogEntry(Model.LogEntry, Model.Event);

            Model.LogEntry.ClearProperties();
        }

        #endregion
    }
}
