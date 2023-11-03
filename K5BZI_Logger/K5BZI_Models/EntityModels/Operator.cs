using System.Windows;
using K5BZI_Models.Attributes;
using K5BZI_Models.Base;
using Newtonsoft.Json;
using PropertyChanged;

namespace K5BZI_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Operator : BaseModel
    {
        public Operator()
        {
            IsClub = false;
        }

        public bool IsActive { get; set; }

        [JsonProperty(Required = Required.Always)]
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

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ClubCall { get; set; }

        [Cabrillo("NAME")]
        public string FullName
        {
            get
            {
                return !IsClub ? FirstName + " " + LastName : ClubName;
            }
        }

        private string _firstName;
        [JsonProperty(Required = Required.Always)]
        public string FirstName
        {
            get
            {
                return IsClub ?
                    ClubName.Trim() :
                    _firstName?.Trim();
            }
            set
            {
                _firstName = value;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ClubName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("ADDRESS")]
        public string Address { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("ADDRESS-CITY")]
        public string City { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("ADDRESS-STATE-PROVINCE")]
        public string State { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("ADDRESS-POSTALCODE")]
        public string ZipCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Cabrillo("ADDRESS-COUNTRY")]
        public string Country { get; set; }
    }
}
