using System.ComponentModel;
using K5BZI_Models.Extensions;

namespace K5BZI_Models.Enums
{
    [TypeConverter(typeof(EnumDescriptionConverter))]
    public enum Assisted
    {
        [Description("Yes")]
        Assisted,

        [Description("No")]
        NONAssisted
    }
}
