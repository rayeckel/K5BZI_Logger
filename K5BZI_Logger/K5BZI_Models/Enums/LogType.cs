using System.ComponentModel;
using K5BZI_Models.Extensions;

namespace K5BZI_Models.Enums
{
    [TypeConverter(typeof(EnumDescriptionConverter))]
    public enum LogType
    {
        [Description("Adif")]
        ADIF,

        [Description("Cabrillo")]
        CABRILLO,

        [Description("Json")]
        JSON
    }
}
