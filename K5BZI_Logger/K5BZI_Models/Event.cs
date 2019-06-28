using K5BZI_Models.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Event
    {
        public Event()
        {
            //Operators = new List<string>();

            //REMOVE THESE
            DXCC = 291;
            CqZone = 05;
            ItuZone = 08;
            ARRL_Sect = "STX";
            State = "TX";
            Class = "5A";
            Club = new Operator { Name = "Oak Hill Amateur Radio Club", IsClub = true, CallSign = "N5OAK" };
            Operators = new List<string> { "N5OAK", "K5BZI", "KC5IHO" };
        }

        public int Id { get; set; }

        public List<string> Operators { get; set; }

        [Cabrillo("OPERATORS")]
        public string OperatorNameList
        {
            get
            {
                var callString = String.Empty;

                Operators.ForEach(_ => callString += String.Format("{0}, ", _));

                return callString;
            }
        }

        [Adif("Contest_ID")]
        [Cabrillo("CONTEST")]
        public string EventName { get; set; }

        [Cabrillo("CLUB")]
        public string ClubName
        {
            get
            {
                return Club != null ? Club.Name : String.Empty;
            }
        }

        public Operator Club { get; set; }

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
