using K5BZI_Models.Attributes;
using PropertyChanged;
using System;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Event
    {
        public int Id { get; set; }

        public string EventName { get; set; }

        public bool IsActive { get; set; }

        public string LogFileName { get; set; }

        public DateTime CreatedDate { get; set; }

        [Adif("Contest_ID")]
        public string ContestId { get; set; }

        [Adif("DXCC")]
        public string DXCC { get; set; }

        [Adif("CQz")]
        public string CqZone { get; set; }

        [Adif("Class")]
        public string Class { get; set; }

        [Adif("ITUz")]
        public string ItuZone { get; set; }

        [Adif("ARRL_Sect")]
        public string ARRL_Sect { get; set; }

        [Adif("State")]
        public string State { get; set; }
    }
}
