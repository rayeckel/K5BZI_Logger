using K5BZI_Models.Base;
using PropertyChanged;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class LogEntry : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime? ContactTime { get; set; }
        public string CallSign { get; set; }
        public Signal Signal { get; set; }
        public SignalReport SignalReport { get; set; }

        public LogEntry()
        {
            Signal = new Signal();
            SignalReport = new SignalReport();
        }

        public Action CheckDuplicateEntriesAction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (propertyName == "CallSign" && CheckDuplicateEntriesAction != null)
            {
                CheckDuplicateEntriesAction.Invoke();
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
