using K5BZI_Models.Attributes;
using K5BZI_Models.Base;
using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Operator : BaseModel
    {
        public bool Selected { get; set; }

        [Cabrillo("CALLSIGN")]
        public string CallSign { get; set; }

        public bool IsClub { get; set; }

        public string ClubCall { get; set; }

        [Cabrillo("NAME")]
        public string FullName
        {
            get
            {
                return !IsClub ? FirstName + " " + LastName : ClubName;
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ClubName { get; set; }

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
