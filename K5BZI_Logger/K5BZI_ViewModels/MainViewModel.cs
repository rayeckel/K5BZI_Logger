using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_Models.Main;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace K5BZI_ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        #region Properties

        public MainModel Model { get; private set; }
        private Operator _currentOperator;
        private readonly ILogListingService _logListingService;
        private readonly IDefaultsService _defaultsService;
        private readonly IOperatorsViewModel _operatorsViewModel;

        #endregion

        #region Constructors

        public MainViewModel(
            ILogListingService logListingService,
            IDefaultsService defaultsService,
            IOperatorsViewModel operatorsViewModel)
        {
            _logListingService = logListingService;
            _defaultsService = defaultsService;
            _operatorsViewModel = operatorsViewModel;

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
                logEntries.ForEach(_ =>
                {
                    if (_.Operator == null) _.Operator = new Operator();

                    Model.LogEntries.Add(_);
                    Model.QSOCount++;
                });

                var lastLogEntry = logEntries.Last();

                Model.LogEntry.Signal.Band = lastLogEntry.Signal.Band;
                Model.LogEntry.Signal.Frequency = lastLogEntry.Signal.Frequency;
                Model.LogEntry.Signal.Mode = lastLogEntry.Signal.Mode;
                Model.LogEntry.Power = lastLogEntry.Power;
                Model.LogEntry.Operator = _currentOperator = lastLogEntry.Operator;
                Model.LogEntry.Country = lastLogEntry.Country;
                Model.LogEntry.Continent = lastLogEntry.Continent;
            }
            else
            {
                Model.LogEntry.Operator = _currentOperator = selectedEvent.Operators.FirstOrDefault();
            }

            UpdateDataGridVisibilities();
        }

        public void CreateNewLog(Event newEvent)
        {
            Model.Event = newEvent;
            Model.LogEntries.Clear();
            Model.LogEntry.ClearProperties(Model.ContactTimeEnabled);
            Model.LogEntry.EventId = newEvent.Id;
            Model.LogEntry.Operator = _currentOperator = newEvent.Operators.First();
        }

        #endregion

        #region Private Methods

        private void UpdateDataGridVisibilities()
        {
            Model.CountryVisibility = Model.LogEntries.Any(_ => !String.IsNullOrEmpty(_.Country)) ? Visibility.Visible : Visibility.Collapsed;
            Model.ContinentVisibility = Model.LogEntries.Any(_ => !String.IsNullOrEmpty(_.Continent)) ? Visibility.Visible : Visibility.Collapsed;
            Model.CQZoneVisibility = Model.LogEntries.Any(_ => !String.IsNullOrEmpty(_.CQZone)) ? Visibility.Visible : Visibility.Collapsed;
        }

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
                CreateNewEntryAction = (_) => Model.LogEntry.ClearProperties(Model.ContactTimeEnabled),
                ViewFileStoreAction = (_) => _logListingService.OpenLogListing(),
                LogItAction = (_) => SaveLogEntry(),
                ManualTimeAction = (_) => SetManualTime(),
                AutoTimeAction = (_) => SetAutoTime(),
                EditLogEntryAction = (_) => EditLogEntry(),
                DeleteLogEntryAction = (_) => DeleteLogEntry(),
                LostFocusAction = (_) => ExecuteLostFocusCommand(_),
                CheckDuplicateEntriesAction = (_) => CheckForDuplicates()
            };

            _defaultsService.SetDefaults(Model.LogEntry);

            UpdateDataGridVisibilities();
        }

        private void ExecuteLostFocusCommand(object argument)
        {
            //reset:
            if (String.IsNullOrEmpty(Model.LogEntry.SignalReport.Sent))
                Model.LogEntry.SignalReport.Sent = "599";
            if (String.IsNullOrEmpty(Model.LogEntry.SignalReport.Received))
                Model.LogEntry.SignalReport.Received = "599";
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
            Model.QSOCount++;
            Model.LogEntry.ClearProperties(Model.ContactTimeEnabled);

            UpdateDataGridVisibilities();
        }

        private void SetManualTime()
        {
            Model.ContactTimeEnabled = true;
            Model.ManualTimeButtonVisibility = Visibility.Collapsed;
            Model.AutoTimeButtonVisibility = Visibility.Visible;

            Model.Timer.Stop();
            Model.LogEntry.ContactTime = null;
        }

        private void SetAutoTime()
        {
            Model.ContactTimeEnabled = false;
            Model.AutoTimeButtonVisibility = Visibility.Collapsed;
            Model.ManualTimeButtonVisibility = Visibility.Visible;

            Model.Timer.Start();
        }

        private void CreateNewLogEntry()
        {
            _logListingService.UpdateLogEntry(Model.SelectedEntry, Model.Event);
        }

        private void EditLogEntry()
        {
            _logListingService.UpdateLogEntry(Model.SelectedEntry, Model.Event);

            UpdateDataGridVisibilities();
        }

        private void DeleteLogEntry()
        {
            var deleteConfirmName = String.Format("Delete {0}?", Model.SelectedEntry.CallSign);
            var confirmResult = MessageBox.Show(deleteConfirmName, "Are you sure?", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                _logListingService.DeleteLogEntry(Model.SelectedEntry, Model.Event);

                Model.LogEntries.Remove(Model.SelectedEntry);

                Model.QSOCount--;
            }
        }

        #endregion
    }
}
