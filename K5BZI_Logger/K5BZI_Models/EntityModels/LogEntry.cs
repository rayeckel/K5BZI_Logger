using System;
using System.Globalization;
using K5BZI_Models.Attributes;
using K5BZI_Models.Enums;
using Newtonsoft.Json;
using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class LogEntry
    {
        public LogEntry()
        {
            Id = Guid.NewGuid();
            Signal = new Signal();
            SignalReport = new SignalReport();
            Operator = new Operator();
        }

        [JsonProperty(Required = Required.Always)]
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

        [JsonProperty(Required = Required.Always)]
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

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("PFX")]
        public string Prefix { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("CONT")]
        public string Continent { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("DXCC")]
        public string Country { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("CQZone")]
        public string CQZone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("QSL_SENT")]
        public string QslSent { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("QSL_RCVD")]
        public string QslReceived { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("CATEGORY-ASSISTED")]
        public Assisted Assisted { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("CATEGORY-POWER")]
        public Power Power { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Operator Operator { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Guid EventId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Signal Signal { get; set; }

        public SignalReport SignalReport { get; set; }

        [Adif("SIG")]
        [JsonIgnore]
        public string Sig
        {
            get { return !String.IsNullOrEmpty(SigInfo) ? "POTA" : null; }
        }

        [Adif("SIG_INFO")]
        public string SigInfo { get; set; }
    }
}
