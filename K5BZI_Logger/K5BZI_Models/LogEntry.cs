using PropertyChanged;
using System;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class LogEntry
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
    }
}
