using K5BZI_Models.Base;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    public class SelectEventModel : BaseModel
    {
        public SelectEventModel()
        {
            ExistingLogs = new ObservableCollection<LogListing>();
        }

        public LogListing SelectedItem { get; set; }
        public ObservableCollection<LogListing> ExistingLogs { get; private set; }

        private ICommand _selectLogCommand;
        public ICommand SelectLogCommand
        {
            get
            {
                return _selectLogCommand ?? (_selectLogCommand = new CommandHandler(SelectLogAction, true));
            }
        }
        public Action SelectLogAction { get; set; }

        private ICommand _createNewLogCommand;
        public ICommand CreateNewLogCommand
        {
            get
            {
                return _createNewLogCommand ??
                    (_createNewLogCommand = new CommandHandler(CreateNewLogAction, true));
            }
        }
        public Action CreateNewLogAction { get; set; }
    }
}
