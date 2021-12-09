using System;
using System.ComponentModel;

namespace K5BZI_Models.Enums
{
    public enum LogType
    {
        [Description("Adif")]
        Adif,

        [Description("Cabrillo")]
        Cabrillo,

        [Description("Json")]
        Json
    }
}
