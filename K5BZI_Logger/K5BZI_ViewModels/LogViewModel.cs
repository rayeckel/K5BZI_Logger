﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using K5BZI_Models;
using K5BZI_Models.Enums;
using K5BZI_Models.Extensions;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using MessageBox = System.Windows.Forms.MessageBox;

namespace K5BZI_ViewModels
{
    public class LogViewModel : ILogViewModel
    {
        #region Properties

        public LogModel LogModel { get; private set; }

        private readonly IDefaultsService _defaultsService;
        private readonly ILogService _logService;
        private readonly IOperatorViewModel _operatorsViewModel;
        private readonly IEventViewModel _eventViewModel;

        #endregion

        #region Constructors

        public LogViewModel(
            IDefaultsService defaultsService,
            ILogService logListingService,
            IOperatorViewModel operatorsViewModel,
            IEventViewModel eventViewModel)
        {
            _defaultsService = defaultsService;
            _logService = logListingService;
            _operatorsViewModel = operatorsViewModel;
            _eventViewModel = eventViewModel;

            Initialize();
        }

        #endregion

        #region Public Methods

        public void GetLog(Event selectedEvent)
        {
            LogModel.LogFileName = selectedEvent.LogFileName;
            LogModel.LogEntries.Clear();

            var logEntries = _logService.ReadLog(LogModel.LogFileName);

            if (logEntries != null && logEntries.Any())
            {
                logEntries.ForEach(_ =>
                {
                    if (_.Operator == null) _.Operator = new Operator();

                    LogModel.LogEntries.Add(_);
                    LogModel.QSOCount++;
                });

                var lastLogEntry = logEntries.Last();

                LogModel.LogEntry.Signal.Band = lastLogEntry.Signal.Band;
                LogModel.LogEntry.Signal.Frequency = lastLogEntry.Signal.Frequency;
                LogModel.LogEntry.Signal.Mode = lastLogEntry.Signal.Mode;
                LogModel.LogEntry.Power = lastLogEntry.Power;
                LogModel.LogEntry.Country = lastLogEntry.Country;
                LogModel.LogEntry.Continent = lastLogEntry.Continent;
            }

            UpdateDataGridVisibilities();
        }

        #endregion

        #region private Methods

        private void Initialize()
        {
            LogModel = new LogModel
            {
                LogItAction = async (_) => await SaveLogEntryAsync(),
                ManualTimeAction = (_) => SetManualTime(),
                AutoTimeAction = (_) => SetAutoTime(),
                DeleteLogEntryAction = (_) => DeleteLogEntry(),
                LostFocusAction = (_) => ExecuteLostFocusCommand(),
                CheckDuplicateEntriesAction = (_) => CheckForDuplicates(),
                BandChangeAction = (_) => OnBandChanged(),
                FrequencyChangeAction = (_) => OnFrequencyChanged(),
            };

            _defaultsService.SetDefaults(LogModel.LogEntry);

            _eventViewModel.EventModel.CreateLogAction = (_) => CreateNewLog(_);
            _eventViewModel.EventModel.GetLogAction = (_) => GetLog(_);

            UpdateDataGridVisibilities();
        }

        private void UpdateDataGridVisibilities()
        {
            LogModel.CountryVisibility = LogModel.LogEntries.Any(_ => !String.IsNullOrEmpty(_.Country)) ? Visibility.Visible : Visibility.Collapsed;
            LogModel.ContinentVisibility = LogModel.LogEntries.Any(_ => !String.IsNullOrEmpty(_.Continent)) ? Visibility.Visible : Visibility.Collapsed;
            LogModel.CQZoneVisibility = LogModel.LogEntries.Any(_ => !String.IsNullOrEmpty(_.CQZone)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public void CreateNewLog(Event newEvent)
        {
            LogModel.LogFileName = newEvent.LogFileName;
            LogModel.LogEntries.Clear();
            LogModel.LogEntry.ClearProperties(LogModel.ContactTimeEnabled);
            LogModel.LogEntry.EventId = newEvent.Id;
        }

        private void CheckForDuplicates()
        {
            LogModel.DuplicateEntries.Clear();

            if (String.IsNullOrEmpty(LogModel.LogEntry.CallSign))
                return;

            LogModel.LogEntries
                .Where(_ => _.CallSign.StartsWith(LogModel.LogEntry.CallSign))
                .ToList()
                .ForEach(_ => LogModel.DuplicateEntries.Add(_));
        }

        private void ExecuteLostFocusCommand()
        {
            //reset:
            if (String.IsNullOrEmpty(LogModel.LogEntry.SignalReport.Sent))
                LogModel.LogEntry.SignalReport.Sent = "599";
            if (String.IsNullOrEmpty(LogModel.LogEntry.SignalReport.Received))
                LogModel.LogEntry.SignalReport.Received = "599";
        }

        private void OnBandChanged()
        {
            LogModel.LogEntry.Signal.Frequency = String.Empty;
        }

        private void OnFrequencyChanged()
        {
            //Parse out the decimals
            if (LogModel.LogEntry.Signal.Frequency.Count(_ => _ == '.') >= 2)
            {
                var parsedFrequencyStringBuilder = new StringBuilder();

                for (var x = 0; x < LogModel.LogEntry.Signal.Frequency.Length; x++)
                {
                    if (LogModel.LogEntry.Signal.Frequency[x] == '.' && (x < 5 || x > 7))
                        continue;

                    parsedFrequencyStringBuilder.Append(LogModel.LogEntry.Signal.Frequency[x]);
                }

                LogModel.LogEntry.Signal.Frequency = parsedFrequencyStringBuilder.ToString();
            }

            //Figure out which band
            switch (BandFrequency.GetBand(LogModel.LogEntry.Signal.Frequency))
            {
                case IsSixThirtyMeters _:
                    LogModel.LogEntry.Signal.Band = Band.SIXTHIRTYMETERS;
                    break;
                case IsOneSixtyMeters _:
                    LogModel.LogEntry.Signal.Band = Band.ONESIXTYMETERS;
                    break;
                case IsEightyMeters _:
                    LogModel.LogEntry.Signal.Band = Band.EIGHTYMETERS;
                    break;
                case IsSixtyMeters _:
                    LogModel.LogEntry.Signal.Band = Band.SIXTYMETERS;
                    break;
                case IsFortyMeters _:
                    LogModel.LogEntry.Signal.Band = Band.FORTYMETERS;
                    break;
                case IsThirtyMeters _:
                    LogModel.LogEntry.Signal.Band = Band.THIRTYMETERS;
                    break;
                case IsTwentyMeters _:
                    LogModel.LogEntry.Signal.Band = Band.TWENTYMETERS;
                    break;
                case IsSeventeenMeters _:
                    LogModel.LogEntry.Signal.Band = Band.SEVENTEENMETERS;
                    break;
                case IsFifteenMeters _:
                    LogModel.LogEntry.Signal.Band = Band.FIFTEENMETERS;
                    break;
                case IsTwelveMeters _:
                    LogModel.LogEntry.Signal.Band = Band.TWELVEMETERS;
                    break;
                case IsElevenMeters _:
                    LogModel.LogEntry.Signal.Band = Band.ELEVENMETERS;
                    break;
                case IsTenMeters _:
                    LogModel.LogEntry.Signal.Band = Band.TENMETERS;
                    break;
                case IsSixMeters _:
                    LogModel.LogEntry.Signal.Band = Band.SIXMETERS;
                    break;
                case IsTwoMeters _:
                    LogModel.LogEntry.Signal.Band = Band.TWOMETERS;
                    break;
                case IsTwoTwenty _:
                    LogModel.LogEntry.Signal.Band = Band.TWOTWENTY;
                    break;
                case IsFourForty _:
                    LogModel.LogEntry.Signal.Band = Band.SEVENTYCENTEMETERS;
                    break;
                case IsNineHundred _:
                    LogModel.LogEntry.Signal.Band = Band.THIRTYTHREECENTEMETERS;
                    break;
                default:
                    LogModel.LogEntry.Signal.Band = Band.TWENTYTHREECENTEMETERS;
                    break;
            }
        }

        private async Task SaveLogEntryAsync()
        {
            if (String.IsNullOrEmpty(LogModel.LogEntry.CallSign))
            {
                MessageBox.Show("You must provide a valid Call Sign.", "Oops!");
                return;
            }

            LogModel.LogEntry.Operator = _operatorsViewModel.OperatorModel.ActiveEvent.ActiveOperator;
            LogModel.LogEntries.Add(LogModel.LogEntry.Clone());
            LogModel.QSOCount++;
            LogModel.LogEntry.ClearProperties(LogModel.ContactTimeEnabled);

            await _logService.SaveLogAsync(LogModel.LogEntries.ToList(), LogModel.LogFileName);

            UpdateDataGridVisibilities();
        }

        private void SetManualTime()
        {
            LogModel.ContactTimeEnabled = true;
            LogModel.ManualTimeButtonVisibility = Visibility.Collapsed;
            LogModel.AutoTimeButtonVisibility = Visibility.Visible;

            LogModel.Timer.Stop();
            LogModel.LogEntry.ContactTime = null;
        }

        private void SetAutoTime()
        {
            LogModel.ContactTimeEnabled = false;
            LogModel.AutoTimeButtonVisibility = Visibility.Collapsed;
            LogModel.ManualTimeButtonVisibility = Visibility.Visible;

            LogModel.Timer.Start();
        }

        private void DeleteLogEntry()
        {
            var deleteConfirmName = String.Format("Delete {0}?", LogModel.SelectedEntry.CallSign);
            var confirmResult = MessageBox.Show(deleteConfirmName, "Are you sure?", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                LogModel.LogEntries.Remove(LogModel.SelectedEntry);

                LogModel.QSOCount--;

                _logService.SaveLogAsync(LogModel.LogEntries.ToList(), LogModel.LogFileName);
            }
        }
        #endregion
    }
}
