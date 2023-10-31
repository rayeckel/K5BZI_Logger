using K5BZI_Models.Enums;
using Newtonsoft.Json;
using PropertyChanged;
using System.Collections.Generic;

namespace K5BZI_Models.EntityModels
{
    [AddINotifyPropertyChangedInterface]
    public class DXCC
    {
        public DXCC()
        {
            ItuAllocations = new List<string>();
            OtherAmateurPrefixes = new List<string>();
        }

        public DXCC(
            string prefix,
            string entity,
            string continent,
            int ituZone,
            CqZone cqZone,
            double utcOffset,
            string lattitude,
            string longitude,
            List<string> ituAllocations,
            List<string> otherAmateurPrefixes
            )
        {
            Prefix = prefix;
            Entity = entity;
            Continent = continent;
            ItuZone = ituZone;
            CqZone = cqZone;
            UtcOffset = utcOffset;
            Lattitude = lattitude;
            Longitude = longitude;
            ItuAllocations = ituAllocations;
            OtherAmateurPrefixes = otherAmateurPrefixes;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Prefix { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Entity { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Continent { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ItuZone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CqZone CqZone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double UtcOffset { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Lattitude { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Longitude { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ItuAllocations { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> OtherAmateurPrefixes { get; set; }
    }
}
