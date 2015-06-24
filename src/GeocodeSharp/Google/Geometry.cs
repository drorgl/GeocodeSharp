using System.Diagnostics;
using Newtonsoft.Json;

namespace GeocodeSharp.Google
{
    [DebuggerDisplay("Location = {Location}, LocationType = {LocationType}, Viewport = {Viewport}, Bounds = {Bounds}")]
    public class Geometry
    {
        /// <summary>
        /// geocoded latitude,longitude value. For normal address lookups, this field is typically the most important.
        /// </summary>
        [JsonProperty("location")]
        public GeoCoordinate Location { get; set; }

        /// <summary>
        /// additional data about the specified location.
        /// </summary>
        [JsonProperty("location_type")]
        public LocationType LocationType { get; set; }

        /// <summary>
        /// contains the recommended viewport for displaying the returned result, specified as two latitude,longitude values defining the southwest and northeast corner of the viewport bounding box. Generally the viewport is used to frame a result when displaying it to a user.
        /// </summary>
        [JsonProperty("viewport")]
        public GeoViewport Viewport { get; set; }

        /// <summary>
        /// (optionally returned) stores the bounding box which can fully contain the returned result. Note that these bounds may not match the recommended viewport. (For example, San Francisco includes the Farallon islands, which are technically part of the city, but probably should not be returned in the viewport.)
        /// </summary>
        [JsonProperty("bounds")]
        public GeoViewport Bounds { get; set; }
    }
}
