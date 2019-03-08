using K5BZI_Models;
using System.Collections.ObjectModel;

namespace K5BZI_ViewModels.Interfaces
{
    public interface IMainLoggerViewModel
    {
        LogEntry LogEntry { get; }
        ObservableCollection<LogEntry> LogEntries{ get; }
    }
}
