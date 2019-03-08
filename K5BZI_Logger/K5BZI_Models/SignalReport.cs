using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class SignalReport
    {
        public int Sent { get; set; }
        public int Received { get; set; }
    }
}
