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
        private readonly IOperatorService _operatorService;
        private readonly IEventService _eventService;

        #endregion

        #region Constructors

        public MainLoggerViewModel(
            ILogListingService logListingService,
            IOperatorService operatorService,
            IEventService eventService)
        {
            _logListingService = logListingService;
            _operatorService = operatorService;
            _eventService = eventService;

            Initialize();
        }

        #endregion

        #region Public Methods

        public void SelectEvent(Event selectedEvent)
        {
            Model.Event = selectedEvent;
            Model.LogEntries.Clear();
            Model.Operators.Clear();

            var logEntries = _logListingService.ReadLog(Model.Event.LogFileName);

            if (logEntries.Any())
            {
                logEntries.ForEach(_ => Model.LogEntries.Add(_));

                Model.LogEntry.Signal.Band = logEntries.Last().Signal.Band;
                Model.LogEntry.Signal.Frequency = logEntries.Last().Signal.Frequency;
            }

            var operators = _operatorService.GetOperatorsByEvent(Model.Event);

            if (operators.Any())
            {
                operators.ForEach(_ => Model.Operators.Add(_));
                Model.CurrentOperator = operators.First();
            }
        }

        public void CreateNewLog(string eventName)
        {
            var newEvent = _eventService.CreateNewEvent(eventName);

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
                CreateNewEntryAction = () => Model.LogEntry.ClearProperties(),
                ViewFileStoreAction = () => _logListingService.OpenLogListing(),
                LogItAction = () => SaveLogEntry(),
                EditLogEntryAction = () => EditLogEntry(),
                EditOperatorsAction = () => EditOperators()
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

            _logListingService.SaveLogEntry(Model.LogEntry, Model.Event);

            Model.LogEntries.Add(Model.LogEntry.Clone());
            Model.LogEntry.ClearProperties();
        }

        private void EditLogEntry()
        {
            _logListingService.UpdateLogEntry(Model.SelectedEntry, Model.Event);
        }

        private void EditOperators()
        {
            MessageBox.Show("Not Implemented.");
        }

        #endregion
    }
}
