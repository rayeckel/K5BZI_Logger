using K5BZI_Models.Attributes;
using K5BZI_Models.Enums;
using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Signal
    {
        public Signal()
        {
            //TODO: Remove these
            Mode = Mode.SSB;
        }

        [Adif("Band")]
        [Cabrillo("CATEGORY-BAND")]
        public string Band { get; set; }

        [Adif("Mode")]
        [Cabrillo("CATEGORY-MODE")]
        public string ModeDescription => Mode.GetName(typeof(Mode), Mode);

        public Mode Mode { get; set; }

        public double Frequency { get; set; }
    }
}
