using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_ViewModels.Interfaces;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace K5BZI_ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainLoggerViewModel : IMainLoggerViewModel
    {
        public LogEntry LogEntry { get; private set; }
        public ObservableCollection<LogEntry> LogEntries { get; private set; }

        public MainLoggerViewModel()
        {
            LogEntries = new ObservableCollection<LogEntry>();
        }

        public void CreateLogEntry()
        {
            if (LogEntry == null)
                LogEntry = new LogEntry();
            else
                LogEntry.ClearProperties();
        }

        public void SaveLogEntry()
        {
            LogEntry.GetType();
        }

        private void CreateMockLogEntry()
        {
            CreateLogEntry();
            LogEnt
        }
    }
}
