using System.ComponentModel;
using K5BZI_Models.Extensions;

namespace K5BZI_Models.Enums
{
    [TypeConverter(typeof(EnumDescriptionConverter))]
    public enum Band
    {
        [Description("630m")]
        SIXTHIRTYMETERS,

        [Description("160m")]
        ONESIXTYMETERS,

        [Description("80m")]
        EIGHTYMETERS,

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
