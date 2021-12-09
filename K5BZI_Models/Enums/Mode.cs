using System.ComponentModel;

namespace K5BZI_Models.Enums
{
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
