using K5BZI_Models.Enums;
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

        public string Prefix { get; set; }

        public string Entity { get; set; }

        public string Continent { get; set; }

        public int ItuZone { get; set; }

        public CqZone CqZone { get; set; }

        public double UtcOffset { get; set; }

        public string Lattitude { get; set; }

        public string Longitude { get; set; }

        public List<string> ItuAllocations { get; set; }

        public List<string> OtherAmateurPrefixes { get; set; }
    }
}
