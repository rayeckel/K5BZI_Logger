using System.ComponentModel;
using K5BZI_Models.Attributes;
using K5BZI_Models.Enums;
using K5BZI_Models.Extensions;
using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Signal
    {
        public Bands Band { get; set; }

        [Adif("BAND")]
        [Cabrillo("CATEGORY-BAND")]
        public string BandString
        {
            get
            {
                return new EnumDescriptionConverter()
                    .GetEnumDescription(Band);
            }
        }

        [Adif("MODE")]
        [Cabrillo("CATEGORY-MODE")]
        public string ModeDescription => Mode.GetName(typeof(Mode), Mode);

        public Mode Mode { get; set; }

        [Adif("FREQ")]
        public string Frequency { get; set; }

        public enum Bands
        {
            [Description("630m")]
            SIXTHIRTYMETERS = 1,

            [Description("160m")]
            ONESIXTYMETERS = 2,

            [Description("80m")]
            EIGHTYMETERS = 3,

            [Description("60m")]
            SIXTYMETERS,

            [Description("40m")]
            FORTYMETERS,

            [Description("30m")]
            THIRTYMETERS,

            [Description("20m")]
            TWENTYMETERS,

            [Description("17m")]
            SEVENTEENMETERS,

            [Description("15m")]
            FIFTEENMETERS,

            [Description("12m")]
            TWELVEMETERS,

            [Description("11m")]
            ELEVENMETERS,

            [Description("10m")]
            TENMETERS,

            [Description("6m")]
            SIXMETERS,

            [Description("2m")]
            TWOMETERS,

            [Description("1.25m")]
            TWOTWENTY,

            [Description("70cm")]
            SEVENTYCENTEMETERS,

            [Description("33cm")]
            THIRTYTHREECENTEMETERS,

            [Description("23cm")]
            TWENTYTHREECENTEMETERS,

            [Description("13cm")]
            THIRTEENCENTEMETERS,

            [Description("9cm")]
            NINECENTEMETERS,

            [Description("6cm")]
            SIXCENTEMETERS,

            [Description("3cm")]
            THREECENTEMETERS
        }
    }
}
