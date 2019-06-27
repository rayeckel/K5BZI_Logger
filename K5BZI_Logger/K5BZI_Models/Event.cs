using K5BZI_Models.Attributes;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Event
    {
        public Event()
        {
            Operators = new ObservableCollection<Operator>();

            //REMOVE THESE
            DXCC = 291;
            CqZone = 05;
            ItuZone = 08;
            ARRL_Sect = "STX";
            State = "TX";
            Class = "5A";
        }

        public int Id { get; set; }

        public ObservableCollection<Operator> Operators { get; set; }

        [Adif("Contest_ID")]
        [Cabrillo("CONTEST")]
        public string EventName { get; set; }

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
        public string ARRL_Sect { get; set; }

        [Adif("State")]
        public string State { get; set; }

        [Cabrillo("SOAPBOX")]
        public string Comments { get; set; }

        [Cabrillo("CLAIMED-SCORE")]
        public double Score { get; set; }
    }
}
