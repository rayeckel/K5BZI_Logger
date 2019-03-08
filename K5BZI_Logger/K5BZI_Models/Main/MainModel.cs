using K5BZI_Models.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace K5BZI_Models.Main
{
    public class MainModel : BaseModel
    {
        public MainModel()
        {
            _logItCommandCanExecute = true;
            _createNewEntryCanExecute = true;

            LogEntry = new LogEntry();
            LogEntries = new ObservableCollection<LogEntry>();
        }

        public LogEntry LogEntry { get; private set; }
        public ObservableCollection<LogEntry> LogEntries { get; private set; }

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
    }
}
