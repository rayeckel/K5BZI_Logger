using K5BZI_Models;
using K5BZI_Models.Extensions;
using K5BZI_ViewModels.Interfaces;
using PropertyChanged;
using System;
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

        public void SaveLogEntry()
        {
            LogEntry.GetType();
        }

        public void CreateMockLogEntry()
        {
            CreateLogEntry();
            LogEntry.CallSign = "KC5IHO";
            LogEntry.ContactTime = DateTime.Now;
            LogEntry.Event.EventName = "Parks On The Air";
            LogEntry.Signal.Band = "40m";
            LogEntry.Signal.Frequency = 7.225;
            LogEntry.SignalReport.Sent = 599;
            LogEntry.SignalReport.Received = 479;

            LogEntries.Add(LogEntry.Clone());
        }

        private void CreateLogEntry()
        {
            if (LogEntry == null)
                LogEntry = new LogEntry();
            else
                LogEntry.ClearProperties();
        }
    }
}
