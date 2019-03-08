using K5BZI_Models.Base;
using System.Collections.ObjectModel;

namespace K5BZI_Models.Main
{
    public class MainModel : BaseModel, IMainModel
    {
        public LogEntry LogEntry { get; private set; }
        public ObservableCollection<LogEntry> LogEntries { get; private set; }

        public MainModel()
        {
            LogEntry = new LogEntry();
            LogEntries = new ObservableCollection<LogEntry>();
        }
    }
}
