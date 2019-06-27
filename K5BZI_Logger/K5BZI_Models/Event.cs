using K5BZI_Models.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Event
    {
        public Event()
        {
            //Operators = new ObservableCollection<Operator>();

            //REMOVE THESE
            DXCC = 291;
            CqZone = 05;
            ItuZone = 08;
            ARRL_Sect = "STX";
            State = "TX";
            Class = "5A";
            ClubName = " Oak Hill Amateur Radio Club";
            Operators = new ObservableCollection<Operator>
            {
                new Operator{ CallSign = "N5OAK", Name = "Oakie Goatboy", City = "Austin", State = "TX"},
                new Operator{ CallSign = "K5BZI", Name = "Ray Eckel", City = "Austin", State = "TX"},
                new Operator{ CallSign = "KC5IHO", Name = "Jason Zubik", City = "Austin", State = "TX"}
            };
        }

        public int Id { get; set; }

        public ObservableCollection<Operator> Operators { get; set; }

        [Cabrillo("OPERATORS")]
        public string OperatorNameList
        {
            get
            {
                var callString = String.Empty;

                Operators.Select(_ => _.CallSign)
                    .ToList()
                    .ForEach(_ => callString += String.Format("{0}, ", _));

                return callString;
            }
        }

        [Adif("Contest_ID")]
        [Cabrillo("CONTEST")]
        public string EventName { get; set; }

        [Cabrillo("CLUB")]
        public string ClubName{ get; set; }

        public bool IsActive { get; set; }

        public string LogFileName { get; set; }

        public DateTime CreatedDate { get; set; }

        [Adif("DXCC")]
        public int DXCC { get; set; }

        [Adif("CQz")]
        public int CqZone { get; set; }

        [Adif("Class")]
        public string Class { get; set; }

        [Adif("ITUz")]
        public int ItuZone { get; set; }

        [Adif("ARRL_Sect")]
        [Cabrillo("LOCATION")]
        public string ARRL_Sect { get; set; }

        [Adif("State")]
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
