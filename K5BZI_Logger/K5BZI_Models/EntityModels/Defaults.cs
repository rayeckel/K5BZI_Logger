using K5BZI_Models.Enums;
using System;

namespace K5BZI_Models.EntityModels
{
    public class Defaults
    {
        public Defaults()
        {
            QslSent = "N";
            QslReceived = "N";
            Country = "USA";
            Continent = "NA";
            Assisted = Assisted.NONAssisted;
            Power = Power.HIGH;
            Mode = Mode.SSB;
        }

        public String QslSent { get; set; }

        public String QslReceived { get; set; }

        public String Country { get; set; }

        public String Continent { get; set; }

        public Assisted Assisted { get; set; }

        public Power Power { get; set; }

        public Mode Mode { get; set; }
    }
}
