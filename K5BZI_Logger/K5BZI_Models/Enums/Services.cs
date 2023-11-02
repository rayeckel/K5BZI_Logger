using System.ComponentModel;
using K5BZI_Models.Extensions;

namespace K5BZI_Models.Enums
{
    [TypeConverter(typeof(EnumDescriptionConverter))]
    public enum Services
    {
        [Description("POTA")]
        PARKSONTHEAIR,

        [Description("SOTA")]
        SUMMITSONTHEAIR,

        [Description("Summer Field Day")]
        SUMMERFIELDDAY,

        [Description("Winter Field Day")]
        WINTERFIELDDAY,

        [Description("JOTA")]
        JAMBOREEONTHEAIR,
    }
}
