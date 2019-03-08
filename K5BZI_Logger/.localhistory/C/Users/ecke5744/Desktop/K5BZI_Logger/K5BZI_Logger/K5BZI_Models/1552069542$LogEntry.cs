using System;
using System.ComponentModel;

namespace K5BZI_Models
{
    public class LogEntry : INotifyPropertyChanged
    {
        public Event Event { get; set; }
        public DateTime ContactTime { get; set; }
        public string CallSign { get; set; }
        public Signal Signal { get; set; }
        public SignalReport SignalReport { get; set; }
    }
}
