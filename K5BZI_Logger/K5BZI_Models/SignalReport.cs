using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class SignalReport
    {
        public SignalReport()
        {
            Sent = 599;
            Received = 599;
        }

        public int Sent { get; set; }
        public int Received { get; set; }
    }
}
