using System.Diagnostics;
using Newtonsoft.Json;

namespace GeocodeSharp.Google
{
    [DebuggerDisplay("LongName = {LongName}, ShortName = {ShortName}, Types = {string.Join(\",\",Types)}")]
    public class AddressComponent
    {
        /// <summary>
        ///  is the full text description or name of the address component as returned by the Geocoder.
        /// </summary>
        [JsonProperty("long_name")]
        public string LongName { get; set; }

        /// <summary>
        ///  is an abbreviated textual name for the address component, if available. 
        ///  For example, an address component for the state of Alaska may have a 
        ///  long_name of "Alaska" and a short_name of "AK" using the 2-letter postal abbreviation.
        /// </summary>
        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        /// <summary>
        /// an array indicating the type of the address component.
        /// </summary>
        [JsonProperty("types")]
        public GeocodeComponent[] Types { get; set; }
    }
}
