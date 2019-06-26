using K5BZI_Models.Base;
using Prism.Commands;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace K5BZI_Models.Main
{
    [AddINotifyPropertyChangedInterface]
    public class MainModel : BaseModel
    {
        #region Constructors

        public MainModel()
        {
            _logItCommandCanExecute = true;
            _createNewEntryCanExecute = true;

            LogEntry = new LogEntry();
            LogEntries = new ObservableCollection<LogEntry>();
            DuplicateEntries = new ObservableCollection<LogEntry>();
        }

        #endregion

        #region Properties

        public Event Event { get; set; }
        public LogEntry LogEntry { get; private set; }
        public ObservableCollection<LogEntry> LogEntries { get; private set; }
        public ObservableCollection<LogEntry> DuplicateEntries { get; private set; }

        #endregion

        #region Commands

        private bool _logItCommandCanExecute;
        private ICommand _logItCommand;
        public ICommand LogItCommand
        {
            get
            {
                return _logItCommand ?? (_logItCommand = new CommandHandler(LogItAction, _logItCommandCanExecute));
            }
        }
        public Action LogItAction { get; set; }


        private bool _createNewEntryCanExecute;
        private ICommand _createNewEntryCommand;
        public ICommand CreateNewEntryCommand
        {
            get
            {
                return _createNewEntryCommand ??
                    (_createNewEntryCommand = new CommandHandler(CreateNewEntryAction, _createNewEntryCanExecute));
            }
        }
        public Action CreateNewEntryAction { get; set; }

        private ICommand _selectEventCommand;
        public ICommand SelectEventCommand
        {
            get
            {
                return _selectEventCommand ??
                    (_selectEventCommand = new CommandHandler(SelectEventAction, true));
            }
        }
        public Action SelectEventAction { get; set; }

        private ICommand _changeEventCommand;
        public ICommand ChangeEventCommand
        {
            get
            {
                return _changeEventCommand ??
                    (_changeEventCommand = new CommandHandler(ChangeEventAction, true));
            }
        }
        public Action ChangeEventAction { get; set; }

        private ICommand _updateLogEntryCommand;
        public ICommand UpdateLogEntryCommand { get; set; }
        /*{
            get
            {
                return _updateLogEntryCommand ??
                    (_updateLogEntryCommand = new CommandHandler(UpdateLogEntryAction, true));
            }
        }*/

        public Action<object, DataGridCellEditEndingEventArgs> UpdateLogEntryAction { get; set; }

        private ICommand _exportLogCommand;
        public ICommand ExportLogCommand
        {
            get
            {
                return _exportLogCommand ??
                    (_exportLogCommand = new CommandHandler(ExportLogAction, true));
            }
        }
        public Action ExportLogAction { get; set; }

        private ICommand _viewFileStoreCommand;
        public ICommand ViewFileStoreCommand
        {
            get
            {
                return _viewFileStoreCommand ??
                    (_viewFileStoreCommand = new CommandHandler(ViewFileStoreAction, true));
            }
        }
        public Action ViewFileStoreAction { get; set; }

        #endregion
    }
}
