using K5BZI_Models.Attributes;
using K5BZI_Models.Enums;
using K5BZI_Models.Extensions;
using PropertyChanged;
using System;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Signal
    {
        public Band Band { get; set; }

        [Adif("BAND")]
        [Cabrillo("CATEGORY-BAND")]
        public string BandString
        {
            get
            {
                return new EnumBindingSourceExtension()
                    .GetEnumDescription(Band);
            }
        }

        [Adif("MODE")]
        [Cabrillo("CATEGORY-MODE")]
        public string ModeDescription => Mode.GetName(typeof(Mode), Mode);

        public Mode Mode { get; set; }

        [Adif("FREQ")]
        public string Frequency { get; set; }
    }
}
