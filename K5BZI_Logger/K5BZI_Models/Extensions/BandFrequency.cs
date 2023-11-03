using System;
using System.Linq;

namespace K5BZI_Models.Extensions
{
    public class BandFrequency
    {
        public static BandFrequency GetBand(string frequencyString)
        {
            if (String.IsNullOrEmpty(frequencyString)) return null;

            object frequency = null;
            var isInt = false;

            if (frequencyString.Contains('.'))
            {
                frequency = Double.Parse(frequencyString);
                isInt = false;
            }
            else
            {
                frequency = Convert.ToInt32(frequencyString);
                isInt = true;
            }

            if ((isInt && ((int)frequency > 901999))
                || !isInt && (double)frequency > (double)901.999)
                return new IsNineHundred();

            if ((isInt && ((int)frequency > 471999))
                || !isInt && (double)frequency > 471.999)
                return new IsSixThirtyMeters();

            if ((isInt && ((int)frequency > 419999))
                || !isInt && (double)frequency > 419.999)
                return new IsFourForty();

            if ((isInt && ((int)frequency > 218999))
                || !isInt && (double)frequency > 218.999)
                return new IsTwoTwenty();

            if ((isInt && ((int)frequency > 143999))
                || !isInt && (double)frequency > 143.999)
                return new IsTwoMeters();

            if ((isInt && ((int)frequency > 49999))
                || !isInt && (double)frequency > 49.999)
                return new IsSixMeters();

            if ((isInt && ((int)frequency > 27999))
                || !isInt && (double)frequency > 27.999)
                return new IsTenMeters();

            if ((isInt && ((int)frequency > 26300))
                || !isInt && (double)frequency > 26.300)
                return new IsElevenMeters();

            if ((isInt && ((int)frequency > 24889))
                || !isInt && (double)frequency > 24.889)
                return new IsTwelveMeters();

            if ((isInt && ((int)frequency > 20999))
                || !isInt && (double)frequency > 20.999)
                return new IsFifteenMeters();

            if ((isInt && ((int)frequency > 18067))
                || !isInt && (double)frequency > 18.067)
                return new IsSeventeenMeters();

            if ((isInt && ((int)frequency > 13999))
                || !isInt && (double)frequency > 13.999)
                return new IsTwentyMeters();

            if ((isInt && ((int)frequency > 10099))
                || !isInt && (double)frequency > 10.099)
                return new IsThirtyMeters();

            if ((isInt && ((int)frequency > 6999))
                || !isInt && (double)frequency > 6.999)
                return new IsFortyMeters();

            if ((isInt && ((int)frequency > 5330))
                || !isInt && (double)frequency > 5.330)
                return new IsSixtyMeters();

            if ((isInt && ((int)frequency > 3499))
                || !isInt && (double)frequency > 3.499)
                return new IsEightyMeters();

            if ((isInt && ((int)frequency > 1799))
                || !isInt && (double)frequency > 1.799)
                return new IsOneSixtyMeters();

            return new IsTwentyThreeCentimeters();
        }
    }

    public class IsSixThirtyMeters : BandFrequency { }
    public class IsOneSixtyMeters : BandFrequency { }
    public class IsEightyMeters : BandFrequency { }
    public class IsSixtyMeters : BandFrequency { }
    public class IsFortyMeters : BandFrequency { }
    public class IsThirtyMeters : BandFrequency { }
    public class IsTwentyMeters : BandFrequency { }
    public class IsSeventeenMeters : BandFrequency { }
    public class IsFifteenMeters : BandFrequency { }
    public class IsTwelveMeters : BandFrequency { }
    public class IsElevenMeters : BandFrequency { }
    public class IsTenMeters : BandFrequency { }
    public class IsSixMeters : BandFrequency { }
    public class IsTwoMeters : BandFrequency { }
    public class IsTwoTwenty : BandFrequency { }
    public class IsFourForty : BandFrequency { }
    public class IsNineHundred : BandFrequency { }
    public class IsTwentyThreeCentimeters : BandFrequency { }
}
