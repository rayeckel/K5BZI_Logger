using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Event
    {
        public string EventName { get; set; }
    }
}
