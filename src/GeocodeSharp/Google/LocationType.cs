using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace GeocodeSharp.Google
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LocationType
    {
        // in case the server returns back something unknown
        Unknown= 0,
        /// <summary>
        /// precise geocode for which we have location information accurate down to street address precision.
        /// </summary>
        [EnumMember(Value ="ROOFTOP")]
        Rooftop = 1 << 0,
        /// <summary>
        /// reflects an approximation (usually on a road) interpolated between two precise points (such as intersections). Interpolated results are generally returned when rooftop geocodes are unavailable for a street address.
        /// </summary>
        [EnumMember(Value = "RANGE_INTERPOLATED")]
        RangeInterpolated = 1 << 1,
        /// <summary>
        ///  is the geometric center of a result such as a polyline (for example, a street) or polygon (region).
        /// </summary>
        [EnumMember(Value = "GEOMETRIC_CENTER")]
        GeometricCenter = 1 << 2,
        /// <summary>
        /// approximate
        /// </summary>
        [EnumMember(Value = "APPROXIMATE")]
        Approximate = 1 << 3,
        
        
    }
}
