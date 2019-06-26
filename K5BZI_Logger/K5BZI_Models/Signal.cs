using K5BZI_Models.Attributes;
using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Signal
    {
        [Adif("Band")]
        public string Band { get; set; }

        [Adif("Mode")]
        public string Mode { get; set; }

        public double Frequency { get; set; }
    }
}
