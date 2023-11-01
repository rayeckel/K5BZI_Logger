using System.ComponentModel;
using K5BZI_Models.Extensions;

namespace K5BZI_Models.Enums
{
    [TypeConverter(typeof(EnumDescriptionConverter))]
    public enum Mode
    {
        [Description("CW")]
        CW,

        [Description("DIGI")]
        DG,

        [Description("FM")]
        FM,

        [Description("RTTY")]
        RTTY,

        [Description("SSB")]
        SSB,

        [Description("Mixed")]
        MIXED
    }
}
