using System.ComponentModel;
using K5BZI_Models.Extensions;

namespace K5BZI_Models.Enums
{
    [TypeConverter(typeof(EnumDescriptionConverter))]
    public enum Mode
    {
        [Description("CW")]
        CW,

        [Description("SSB")]
        SSB,

        [Description("DIGI")]
        DG,

        [Description("FM")]
        FM,

        [Description("RTTY")]
        RTTY,

        [Description("FT8")]
        FT8,

        [Description("FT4")]
        FT4,

        [Description("PSK31")]
        PSK31,

        [Description("SSTV")]
        SSTV,

        [Description("Mixed")]
        MIXED
    }
}
