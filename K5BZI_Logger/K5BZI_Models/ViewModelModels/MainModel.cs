using K5BZI_Models.Base;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace K5BZI_Models.Main
{
    [AddINotifyPropertyChangedInterface]
    public class MainModel : BaseViewModel
    {
        #region Constructors

        public MainModel()
        {
            LogEntry = new LogEntry();
            LogEntries = new ObservableCollection<LogEntry>();
            DuplicateEntries = new ObservableCollection<LogEntry>();
        }

        #endregion

        #region Properties

        public Event Event { get; set; }
        public LogEntry LogEntry { get; private set; }
        public LogEntry SelectedEntry { get; set; }
        public ObservableCollection<LogEntry> LogEntries { get; private set; }
        public ObservableCollection<LogEntry> DuplicateEntries { get; private set; }

        #endregion

        #region Commands

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


        private ICommand _createNewEntryCommand;
        public ICommand CreateNewEntryCommand
        {
            get
            {
                return _createNewEntryCommand ??
                    (_createNewEntryCommand =
                    new DelegateCommand(CreateNewEntryAction, _ => { return true; }));
            }
        }
        public Action<object> CreateNewEntryAction { get; set; }

        private ICommand _updateLogEntryCommand;
        public ICommand UpdateLogEntryCommand
        {
            get
            {
                return _updateLogEntryCommand ??
                    (_updateLogEntryCommand =
                    new DelegateCommand(EditLogEntryAction, _ => { return SelectedEntry != null; }));
            }
        }

        public Action<object> EditLogEntryAction { get; set; }

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

        private ICommand _viewFileStoreCommand;
        public ICommand ViewFileStoreCommand
        {
            get
            {
                return _viewFileStoreCommand ??
                    (_viewFileStoreCommand = new DelegateCommand(ViewFileStoreAction, _ => { return true; }));
            }
        }
        public Action<object> ViewFileStoreAction { get; set; }

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
        #endregion
    }
}
