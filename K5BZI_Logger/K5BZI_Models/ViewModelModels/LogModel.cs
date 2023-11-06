using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using K5BZI_Models.Enums;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class LogModel
    {
        #region Constructors

        public LogModel()
        {
            LogEntry = new LogEntry();
            LogEntry.Assisted = Assisted.NONAssisted;
            LogEntries = new ObservableCollection<LogEntry>();
            DuplicateEntries = new ObservableCollection<LogEntry>();

            AutoTimeButtonVisibility = Visibility.Collapsed;
        }

        #endregion

        #region Properties

        public string LogFileName { get; set; }
        public bool ContactTimeEnabled { get; set; }
        public int QSOCount { get; set; }
        public DispatcherTimer Timer { get; set; }
        public LogEntry LogEntry { get; private set; }
        public LogEntry SelectedEntry { get; set; }
        public ObservableCollection<LogEntry> LogEntries { get; private set; }
        public ObservableCollection<LogEntry> DuplicateEntries { get; private set; }
        public Visibility ManualTimeButtonVisibility { get; set; }
        public Visibility AutoTimeButtonVisibility { get; set; }
        public Visibility CountryVisibility { get; set; }
        public Visibility ContinentVisibility { get; set; }
        public Visibility CQZoneVisibility { get; set; }
        public Visibility IsLogitVisibility { get; set; }
        public IEnumerable<Band> BandValues
        {
            get { return Enum.GetValues(typeof(Band)).Cast<Band>(); }
        }
        public IEnumerable<Mode> ModeValues
        {
            get { return Enum.GetValues(typeof(Mode)).Cast<Mode>(); }
        }
        public IEnumerable<Assisted> AssistedValues
        {
            get { return Enum.GetValues(typeof(Assisted)).Cast<Assisted>(); }
        }

        #endregion

        #region Commands

        private ICommand _lostFocusCommand;
        public ICommand LostFocusCommand
        {

            get
            {
                return _lostFocusCommand ?? (_lostFocusCommand =
                    new DelegateCommand(LostFocusAction, _ => { return true; }));
            }
        }

        public Action<object> LostFocusAction { get; set; }

        private ICommand _logItCommand;
        public ICommand LogItCommand
        {
            get
            {
                return _logItCommand ?? (_logItCommand =
                    new DelegateCommand(LogItAction, _ => { return true; }));
            }
        }
        public Action<object> LogItAction { get; set; }

        private ICommand _manualTimeCommand;
        public ICommand ManualTimeCommand
        {
            get
            {
                return _manualTimeCommand ?? (_manualTimeCommand =
                    new DelegateCommand(ManualTimeAction, _ => { return true; }));
            }
        }
        public Action<object> ManualTimeAction { get; set; }

        private ICommand _autoTimeCommand;
        public ICommand AutoTimeCommand
        {
            get
            {
                return _autoTimeCommand ?? (_autoTimeCommand =
                    new DelegateCommand(AutoTimeAction, _ => { return true; }));
            }
        }
        public Action<object> AutoTimeAction { get; set; }

        private ICommand _selectExportLogCommand;
        public ICommand SelectExportLogCommand
        {
            get
            {
                return _selectExportLogCommand ??
                    (_selectExportLogCommand = new DelegateCommand(SelectExportLogAction, _ => { return true; }));
            }
        }
        public Action<object> SelectExportLogAction { get; set; }

        private ICommand _submitLogCommand;
        public ICommand SubmitLogCommand
        {
            get
            {
                return _submitLogCommand ??
                    (_submitLogCommand = new DelegateCommand(SubmitLogAction, _ => { return true; }));
            }
        }
        public Action<object> SubmitLogAction { get; set; }

        private ICommand _deleteLogEntryCommand;
        public ICommand DeleteLogEntryCommand
        {
            get
            {
                return _deleteLogEntryCommand ??
                    (_deleteLogEntryCommand =
                    new DelegateCommand(DeleteLogEntryAction, _ => { return SelectedEntry != null; }));
            }
        }
        public Action<object> DeleteLogEntryAction { get; set; }

        private ICommand _checkDuplicateEntriesCommand;
        public ICommand CheckDuplicateEntriesCommand
        {
            get
            {
                return _checkDuplicateEntriesCommand ??
                    (_checkDuplicateEntriesCommand =
                    new DelegateCommand(CheckDuplicateEntriesAction));
            }
        }
        public Action<object> CheckDuplicateEntriesAction { get; set; }

        private ICommand _bandChangeCommand;
        public ICommand BandChangeCommand
        {
            get
            {
                return _bandChangeCommand ?? (_bandChangeCommand =
                    new DelegateCommand(BandChangeAction, _ => { return true; }));
            }
        }
        public Action<object> BandChangeAction { get; set; }

        private ICommand _frequencyChangeCommand;
        public ICommand FrequencyChangeCommand
        {
            get
            {
                return _frequencyChangeCommand ?? (_frequencyChangeCommand =
                    new DelegateCommand(FrequencyChangeAction, _ => { return true; }));
            }
        }
        public Action<object> FrequencyChangeAction { get; set; }

        #endregion
    }
}
