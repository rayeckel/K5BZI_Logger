using K5BZI_Models.Attributes;
using K5BZI_Models.Base;
using PropertyChanged;
using System.Windows;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Operator : BaseModel
    {
        public bool Selected { get; set; }

        [Cabrillo("CALLSIGN")]
        public string CallSign { get; set; }

        private bool _isClub;
        public bool IsClub
        {
            get
            {
                return _isClub;
            }
            set
            {
                _isClub = value;
            }
        }

        [AlsoNotifyFor("IsClub")]
        public string EditOperatorTitle
        {
            get
            {
                return IsClub ?
                    "Edit Club" :
                    "Edit Operator";
            }
        }

        [AlsoNotifyFor("IsClub")]
        public Visibility IsClubVisibility
        {
            get
            {
                return IsClub ?
                    Visibility.Visible :
                    Visibility.Collapsed;
            }
        }

        [AlsoNotifyFor("IsClub")]
        public Visibility IsNotClubVisibility
        {
            get
            {
                return !IsClub ?
                    Visibility.Visible :
                    Visibility.Collapsed;
            }
        }

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
