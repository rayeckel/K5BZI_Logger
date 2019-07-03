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
    public class MainViewModel : IMainViewModel
    {
        #region Properties

        public MainModel Model { get; private set; }
        private Operator _currentOperator;
        private readonly ILogListingService _logListingService;
        private readonly IDefaultsService _defaultsService;

        #endregion

        #region Constructors

        public MainViewModel(
            ILogListingService logListingService,
            IDefaultsService defaultsService)
        {
            _logListingService = logListingService;
            _defaultsService = defaultsService;

            Initialize();
        }

        #endregion

        #region Public Methods

        public void SelectEvent(Event selectedEvent)
        {
            Model.Event = selectedEvent;
            Model.LogEntries.Clear();

            var logEntries = _logListingService.ReadLog(Model.Event.LogFileName);

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

        public void UpdateCurrentOperator(Operator currentOperator)
        {
            _currentOperator = currentOperator;
        }

        #endregion

        #region Private Methods

        private void CheckForDuplicates()
        {
            Model.DuplicateEntries.Clear();

            if (String.IsNullOrEmpty(Model.LogEntry.CallSign))
                return;

            Model.LogEntries
                .Where(_ => _.CallSign.StartsWith(Model.LogEntry.CallSign))
                .ToList()
                .ForEach(_ => Model.DuplicateEntries.Add(_));
        }

        private void Initialize()
        {
            Model = new MainModel
            {
                CreateNewEntryAction = (_) => Model.LogEntry.ClearProperties(),
                ViewFileStoreAction = (_) => _logListingService.OpenLogListing(),
                LogItAction = (_) => SaveLogEntry(),
                EditLogEntryAction = (_) => EditLogEntry(),
                DeleteLogEntryAction = (_) => DeleteLogEntry()
            };

            _defaultsService.SetDefaults(Model.LogEntry);

            Model.LogEntry.CheckDuplicateEntriesAction = () => CheckForDuplicates();
        }

        private void SaveLogEntry()
        {
            if (String.IsNullOrEmpty(Model.LogEntry.CallSign))
            {
                MessageBox.Show("You must provide a valid Call Sign.", "Oops!");
                return;
            }

            Model.LogEntry.Operator = _currentOperator;

            _logListingService.SaveLogEntry(Model.LogEntry, Model.Event);

            Model.LogEntries.Add(Model.LogEntry.Clone());
            Model.LogEntry.ClearProperties();
        }

        private void EditLogEntry()
        {
            _logListingService.UpdateLogEntry(Model.SelectedEntry, Model.Event);
        }

        private void DeleteLogEntry()
        {
            var deleteConfirmName = String.Format("Delete {0}?", Model.SelectedEntry.CallSign);
            var confirmResult = MessageBox.Show(deleteConfirmName, "Are you sure?", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                _logListingService.DeleteLogEntry(Model.SelectedEntry, Model.Event);

                Model.LogEntries.Remove(Model.SelectedEntry);
            }
        }

        #endregion
    }
}
