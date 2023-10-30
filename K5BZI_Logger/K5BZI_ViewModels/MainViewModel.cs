using K5BZI_Models;
using K5BZI_Models.Enums;
using K5BZI_Models.Extensions;
using K5BZI_Models.Main;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace K5BZI_ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        #region Properties

        public MainModel Model { get; private set; }
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
                Model.LogEntry.Country = lastLogEntry.Country;
                Model.LogEntry.Continent = lastLogEntry.Continent;
            }

            UpdateDataGridVisibilities();
        }

        public void CreateNewLog(Event newEvent)
        {
            Model.Event = newEvent;
            Model.LogEntries.Clear();
            Model.LogEntry.ClearProperties(Model.ContactTimeEnabled);
            Model.LogEntry.EventId = newEvent.Id;
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
                LostFocusAction = (_) => ExecuteLostFocusCommand(),
                CheckDuplicateEntriesAction = (_) => CheckForDuplicates(),
                BandChangeAction = (_) => OnBandChanged(),
                FrequencyChangeAction = (_) => OnFrequencyChanged(),
            };

            _defaultsService.SetDefaults(Model.LogEntry);

            UpdateDataGridVisibilities();
        }

        private void ExecuteLostFocusCommand()
        {
            //reset:
            if (String.IsNullOrEmpty(Model.LogEntry.SignalReport.Sent))
                Model.LogEntry.SignalReport.Sent = "599";
            if (String.IsNullOrEmpty(Model.LogEntry.SignalReport.Received))
                Model.LogEntry.SignalReport.Received = "599";
        }

        private void OnBandChanged()
        {
            Model.LogEntry.Signal.Frequency = String.Empty;
        }

        private void OnFrequencyChanged()
        {
            //Parse out the decimals
            if (Model.LogEntry.Signal.Frequency.Count(_ => _ == '.') >= 2)
            {
                var parsedFrequencyStringBuilder = new StringBuilder();

                for (var x = 0; x < Model.LogEntry.Signal.Frequency.Length; x++)
                {
                    if (Model.LogEntry.Signal.Frequency[x] == '.' && (x < 5 || x > 7))
                        continue;

                    parsedFrequencyStringBuilder.Append(Model.LogEntry.Signal.Frequency[x]);
                }

                Model.LogEntry.Signal.Frequency = parsedFrequencyStringBuilder.ToString();
            }

            //Figure out which band
            switch (BandFrequency.GetBand(Model.LogEntry.Signal.Frequency))
            {
                case IsSixThirtyMeters _:
                    Model.LogEntry.Signal.Band = Band.SIXTHIRTYMETERS;
                    break;
                case IsOneSixtyMeters _:
                    Model.LogEntry.Signal.Band = Band.ONESIXTYMETERS;
                    break;
                case IsEightyMeters _:
                    Model.LogEntry.Signal.Band = Band.EIGHTYMETERS;
                    break;
                case IsSixtyMeters _:
                    Model.LogEntry.Signal.Band = Band.SIXTYMETERS;
                    break;
                case IsFortyMeters _:
                    Model.LogEntry.Signal.Band = Band.FORTYMETERS;
                    break;
                case IsThirtyMeters _:
                    Model.LogEntry.Signal.Band = Band.THIRTYMETERS;
                    break;
                case IsTwentyMeters _:
                    Model.LogEntry.Signal.Band = Band.TWENTYMETERS;
                    break;
                case IsSeventeenMeters _:
                    Model.LogEntry.Signal.Band = Band.SEVENTEENMETERS;
                    break;
                case IsFifteenMeters _:
                    Model.LogEntry.Signal.Band = Band.FIFTEENMETERS;
                    break;
                case IsTwelveMeters _:
                    Model.LogEntry.Signal.Band = Band.TWELVEMETERS;
                    break;
                case IsElevenMeters _:
                    Model.LogEntry.Signal.Band = Band.ELEVENMETERS;
                    break;
                case IsTenMeters _:
                    Model.LogEntry.Signal.Band = Band.TENMETERS;
                    break;
                case IsSixMeters _:
                    Model.LogEntry.Signal.Band = Band.SIXMETERS;
                    break;
                case IsTwoMeters _:
                    Model.LogEntry.Signal.Band = Band.TWOMETERS;
                    break;
                case IsTwoTwenty _:
                    Model.LogEntry.Signal.Band = Band.TWOTWENTY;
                    break;
                case IsFourForty _:
                    Model.LogEntry.Signal.Band = Band.SEVENTYCENTEMETERS;
                    break;
                case IsNineHundred _:
                    Model.LogEntry.Signal.Band = Band.THIRTYTHREECENTEMETERS;
                    break;
                default:
                    Model.LogEntry.Signal.Band = Band.TWENTYTHREECENTEMETERS;
                    break;
            }
        }

        private void SaveLogEntry()
        {
            if (String.IsNullOrEmpty(Model.LogEntry.CallSign))
            {
                MessageBox.Show("You must provide a valid Call Sign.", "Oops!");
                return;
            }

            Model.LogEntry.Operator = _operatorsViewModel.Model.CurrentOperator;

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
