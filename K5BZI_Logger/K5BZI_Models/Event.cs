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
    }
}
