using System;
using System.Collections.ObjectModel;
using System.Linq;
using K5BZI_Models.Attributes;
using K5BZI_Models.Base;
using K5BZI_Models.EntityModels;
using K5BZI_Models.Enums;
using Newtonsoft.Json;
using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Event : BaseModel
    {
        public Event()
        {
            Operators = new ObservableCollection<Operator>();
        }

        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ObservableCollection<Operator> Operators { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("OPERATORS")]
        public string OperatorNameList
        {
            get
            {
                var callString = String.Empty;

                Operators.ToList()
                    .ForEach(_ => callString += String.Format("{0}, ", _.FullName));

                return callString;
            }
        }

        [JsonProperty(Required = Required.Always)]
        [Adif("CONTEST_ID")]
        [Cabrillo("CONTEST")]
        public string EventName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("CLUB")]
        public string ClubName
        {
            get
            {
                return Club != null ?
                    Club.FullName :
                    String.Empty;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Operator Club { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string LogFileName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DXCC DXCC { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("CQZ")]
        public CqZone CqZone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("CLASS")]
        public string Class { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("ITUZ")]
        public int ItuZone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("ARRL_SECT")]
        [Cabrillo("LOCATION")]
        public ArrlSection ARRL_Sect { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Adif("STATE")]
        public string State { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("SOAPBOX")]
        public string Comments { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("CLAIMED-SCORE")]
        public double Score { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("CATEGORY-OVERLAY")]
        public string Overlay { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("CATEGORY-TRANSMITTER")]
        public string TransmitterCount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("CATEGORY-OPERATOR")]
        public int OperatorCount
        {
            get
            {
                return Operators.Count;
            }
        }
    }
}
