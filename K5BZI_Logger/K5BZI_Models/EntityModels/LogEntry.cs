using K5BZI_Models.Attributes;
using K5BZI_Models.Enums;
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
            Signal = new Signal();
            SignalReport = new SignalReport();
        }

        public DateTime? ContactTime { get; set; }

        [Adif("QSO_DATE")]
        public string QsoDate
        {
            get
            {
                return ContactTime != null ? ((DateTime)ContactTime).ToString("yyyyMMdd") : String.Empty;
            }
        }

        [Adif("TIME_ON")]
        public string QsoTime
        {
            get
            {
                return ContactTime != null ? ((DateTime)ContactTime).TimeOfDay.ToString() : String.Empty;
            }
        }

        [Adif("CALL")]
        public string CallSign { get; set; }

        [Adif("PFX")]
        public string Prefix { get; set; }

        [Adif("ONT")]
        public string Continent { get; set; }

        [Adif("COUNTRY")]
        public string Country { get; set; }

        [Adif("QSL_SENT")]
        public string QslSent { get; set; }

        [Adif("QSL_RCVD")]
        public string QslReceived { get; set; }

        [Cabrillo("CATEGORY-ASSISTED")]
        public Assisted Assisted { get; set; }

        [Cabrillo("CATEGORY-POWER")]
        public Power Power { get; set; }

        public Operator Operator { get; set; }

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
