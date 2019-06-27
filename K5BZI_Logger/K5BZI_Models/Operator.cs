using K5BZI_Models.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Operator
    {
        [Cabrillo("CALLSIGN")]
        public string CallSign { get; set; }

        [Cabrillo("NAME")]
        public string Name { get; set; }

        [Cabrillo("ADDRESS")]
        public string Address { get; set; }

        [Cabrillo("ADDRESS-CITY")]
        public string City { get; set; }

        [Cabrillo("ADDRESS-STATE-PROVINCE")]
        public string State { get; set; }

        [Cabrillo("ADDRESS-POSTALCODE")]
        public string ZipCode { get; set; }

        [Cabrillo("ADDRESS-COUNTRY")]
        public string Country { get; set; }
    }
}
