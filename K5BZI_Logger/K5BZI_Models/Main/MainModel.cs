using K5BZI_Models.Base;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace K5BZI_Models.Main
{
    [AddINotifyPropertyChangedInterface]
    public class MainModel : BaseModel
    {
        public MainModel()
        {
            _logItCommandCanExecute = true;
            _createNewEntryCanExecute = true;

            LogEntry = new LogEntry();
            LogEntries = new ObservableCollection<LogEntry>();

            MainVisibility = Visibility.Hidden;
        }

        public LogEntry LogEntry { get; private set; }
        public ObservableCollection<LogEntry> LogEntries { get; private set; }

        public Visibility MainVisibility { get; set; }

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
    }
}
