using K5BZI_Models.Attributes;
using K5BZI_Models.Enums;
using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Signal
    {
        [Adif("BAND")]
        [Cabrillo("CATEGORY-BAND")]
        public string Band { get; set; }

        [Adif("MODE")]
        [Cabrillo("CATEGORY-MODE")]
        public string ModeDescription => Mode.GetName(typeof(Mode), Mode);

        public Mode Mode { get; set; }

        [Adif("FREQ")]
        public double Frequency { get; set; }
    }
}
