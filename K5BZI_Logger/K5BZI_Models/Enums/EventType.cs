using System.ComponentModel;
using K5BZI_Models.Extensions;

namespace K5BZI_Models.Enums
{
    [TypeConverter(typeof(EnumDescriptionConverter))]
    public enum EventType
    {
        [Description("Parks On The Air")]
        PARKSONTHEAIR,

        /*
        [Description("Summits On The Air")]
        SUMMITSONTHEAIR,

        [Description("Summer Field Day")]
        SUMMERFIELDDAY,

        [Description("Winter Field Day")]
        WINTERFIELDDAY,

        [Description("Jamboree On The Air")]
        JAMBOREEONTHEAIR,

        [Description("Eleven Meters")]
        ELEVENMETERS,
                        */
        [Description("Custom")]
        CUSTOM
    }
}
