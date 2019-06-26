using K5BZI_Models.Attributes;
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
        public LogEntry()
        {
            Cont = "NA";
            Signal = new Signal();
            SignalReport = new SignalReport();
        }

        [Adif("QSO_Date")]
        public DateTime? ContactTime { get; set; }

        [Adif("Call")]
        public string CallSign { get; set; }

        [Adif("Pfx")]
        public string Prefix { get; set; }

        [Adif("Cont")]
        public string Cont { get; set; }

        [Adif("Country")]
        public string Country { get; set; }

        [Adif("QSL_Sent")]
        public string QslSent { get; set; }

        [Adif("QSL_Rcvd")]
        public string QslReceived { get; set; }

        public int Id { get; set; }

        public int EventId { get; set; }

        public Signal Signal { get; set; }

        public SignalReport SignalReport { get; set; }

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
