using K5BZI_Models.Attributes;
using K5BZI_Models.Enums;
using PropertyChanged;
using System;
using System.Globalization;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class LogEntry
    {
        public LogEntry()
        {
            Signal = new Signal();
            SignalReport = new SignalReport();
            Operator = new Operator();
        }

        public DateTime? ContactTime { get; set; }

        [Adif("QSO_DATE")]
        public string QsoDate
        {
            get
            {
                return ContactTime != null ?
                    ((DateTime)ContactTime).ToUniversalTime().ToString("yyyyMMdd") :
                    String.Empty;
            }
        }

        [Adif("TIME_ON")]
        public string QsoTime
        {
            get
            {
                return ContactTime != null ?
                    ((DateTime)ContactTime).ToUniversalTime().ToString("t", CultureInfo.CreateSpecificCulture("de-DE")).Replace(@":", "") :
                    String.Empty;
            }
        }

        private string _callSign;
        [Adif("CALL")]
        public string CallSign
        {
            get
            {
                return _callSign;
            }
            set
            {
                _callSign = value.ToUpper();
            }
        }

        [Adif("PFX")]
        public string Prefix { get; set; }

        [Adif("CONT")]
        public string Continent { get; set; }

        [Adif("DXCC")]
        public string Country { get; set; }

        [Adif("CQZone")]
        public string CQZone { get; set; }

        [Adif("QSL_SENT")]
        public string QslSent { get; set; }

        [Adif("QSL_RCVD")]
        public string QslReceived { get; set; }

        [Adif("SIG_INFO")]
        public string Notes { get; set; }

        [Cabrillo("CATEGORY-ASSISTED")]
        public Assisted Assisted { get; set; }

        [Cabrillo("CATEGORY-POWER")]
        public Power Power { get; set; }

        public Operator Operator { get; set; }

        public int Id { get; set; }

        public Guid EventId { get; set; }

        public Signal Signal { get; set; }

        public SignalReport SignalReport { get; set; }
    }
}
