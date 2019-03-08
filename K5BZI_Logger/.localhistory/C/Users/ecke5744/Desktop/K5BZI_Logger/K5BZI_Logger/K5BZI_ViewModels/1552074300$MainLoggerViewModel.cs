using K5BZI_Models.Extensions;
using K5BZI_Models.Main;
using K5BZI_ViewModels.Interfaces;
using PropertyChanged;
using System;

namespace K5BZI_ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainLoggerViewModel : IMainLoggerViewModel
    {
        public MainModel Model { get; private set; }

        public MainLoggerViewModel()
        {
            Model = new MainModel();
            Model.CreateNewEntryAction = () => CreateMockLogEntry();
            Model.LogItAction = () => SaveLogEntry();
        }

        public void SaveLogEntry()
        {
            Model.LogEntries.Add(Model.LogEntry.Clone());

            CreateNewLogEntry();
        }

        public void CreateMockLogEntry()
        {
            CreateNewLogEntry();
            Model.LogEntry.CallSign = "KC5IHO";
            Model.LogEntry.ContactTime = DateTime.Now;
            Model.LogEntry.Event.EventName = "Parks On The Air";
            Model.LogEntry.Signal.Band = "40m";
            Model.LogEntry.Signal.Frequency = 7.225;
            Model.LogEntry.SignalReport.Sent = 599;
            Model.LogEntry.SignalReport.Received = 479;
        }

        private void CreateNewLogEntry()
        {
            Model.LogEntry.ClearProperties();
        }
    }
}
