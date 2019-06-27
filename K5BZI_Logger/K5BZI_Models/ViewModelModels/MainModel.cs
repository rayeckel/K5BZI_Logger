using K5BZI_Models.Base;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.PlatformUI;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

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
        public ICommand UpdateLogEntryCommand
        {
            get
            {
                return _updateLogEntryCommand ??
                    (_updateLogEntryCommand = new DelegateCommand(UpdateLogEntryAction));
            }
        }

        public Action<object> UpdateLogEntryAction { get; set; }

        private ICommand _selectExportLogCommand;
        public ICommand SelectExportLogCommand
        {
            get
            {
                return _selectExportLogCommand ??
                    (_selectExportLogCommand = new CommandHandler(SelectExportLogAction, true));
            }
        }
        public Action SelectExportLogAction { get; set; }

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

        private ICommand _editOperatorsCommand;
        public ICommand EditOperatorsCommand
        {
            get
            {
                return _editOperatorsCommand ??
                    (_editOperatorsCommand = new CommandHandler(EditOperatorsAction, true));
            }
        }
        public Action EditOperatorsAction { get; set; }
        #endregion
    }
}
