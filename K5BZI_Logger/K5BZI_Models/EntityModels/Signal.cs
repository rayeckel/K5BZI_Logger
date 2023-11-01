using System;
using System.Collections.Generic;
using System.Linq;
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

        public Mode Mode { get; set; }

        [Adif("FREQ")]
        public string Frequency { get; set; }

        public IEnumerable<Band> BandValues
        {
            get { return Enum.GetValues(typeof(Band)).Cast<Band>(); }
        }

        public IEnumerable<Mode> ModeValues
        {
            get { return Enum.GetValues(typeof(Mode)).Cast<Mode>(); }
        }

        public IEnumerable<Assisted> AssistedValues
        {
            get { return Enum.GetValues(typeof(Assisted)).Cast<Assisted>(); }
        }
    }
}
