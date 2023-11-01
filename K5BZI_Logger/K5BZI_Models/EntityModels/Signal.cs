using K5BZI_Models.Attributes;
using K5BZI_Models.Enums;
using K5BZI_Models.Extensions;
using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Signal
    {
        public Band Band { get; set; }

        public Mode Mode { get; set; }

        [Adif("BAND")]
        [Cabrillo("CATEGORY-BAND")]
        public string BandString
        {
            get
            {
                return new EnumDescriptionConverter(typeof(Band))
                    .GetEnumDescription(Band);
            }
        }

        [Adif("MODE")]
        [Cabrillo("CATEGORY-MODE")]
        public string ModeDescription => Mode.GetName(typeof(Mode), Mode);

        [Adif("FREQ")]
        public string Frequency { get; set; }
    }
}
