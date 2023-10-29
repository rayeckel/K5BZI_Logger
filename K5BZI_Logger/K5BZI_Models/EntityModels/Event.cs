using K5BZI_Models.Attributes;
using K5BZI_Models.EntityModels;
using K5BZI_Models.Enums;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Event
    {
        public Event()
        {
            Operators = new ObservableCollection<Operator>();
            IsActive = true;
        }

        public int Id { get; set; }

        public ObservableCollection<Operator> Operators { get; set; }

        public Operator ActiveOperator { get; set; }

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

        [Adif("CONTEST_ID")]
        [Cabrillo("CONTEST")]
        public string EventName { get; set; }

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

        public Operator Club { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string LogFileName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DXCC DXCC { get; set; }

        [Adif("CQZ")]
        public CqZone CqZone { get; set; }

        [Adif("CLASS")]
        public string Class { get; set; }

        [Adif("ITUZ")]
        public int ItuZone { get; set; }

        [Adif("ARRL_SECT")]
        [Cabrillo("LOCATION")]
        public ArrlSection ARRL_Sect { get; set; }

        [Adif("STATE")]
        public string State { get; set; }

        [Cabrillo("SOAPBOX")]
        public string Comments { get; set; }

        [Cabrillo("CLAIMED-SCORE")]
        public double Score { get; set; }

        [Cabrillo("CATEGORY-OVERLAY")]
        public string Overlay { get; set; }

        [Cabrillo("CATEGORY-TRANSMITTER")]
        public string TransmitterCount { get; set; }

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
