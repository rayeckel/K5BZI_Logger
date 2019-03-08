using PropertyChanged;
using System;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class LogEntry
    {
        public Event Event { get; set; }
        public DateTime? ContactTime { get; set; }
        public string CallSign { get; set; }
        public Signal Signal { get; set; }
        public SignalReport SignalReport { get; set; }

        public LogEntry()
        {
            Event = new Event();
            Signal = new Signal();
            SignalReport = new SignalReport();
        }
    }
}
