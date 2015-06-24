using System.Diagnostics;
using Newtonsoft.Json;

namespace GeocodeSharp.Google
{
    [DebuggerDisplay("Status = {Status}, Results.Length = {Results.Length}, ErrorMessage = {ErrorMessage}")]
    public class GeocodeResponse
    {
        [JsonProperty("results")]
        public GeocodeResult[] Results { get; set; }

        /// <summary>
        /// The "status" field within the Geocoding response object contains the 
        /// status of the request, and may contain debugging information to help 
        /// you track down why geocoding is not working. 
        /// </summary>
        [JsonProperty("status")]
        public GeocodeStatus Status { get; set; }

        /// <summary>
        /// When the geocoder returns a status code other than OK, there may be an 
        /// additional error_message field within the Geocoding response object. 
        /// This field contains more detailed information about the reasons 
        /// behind the given status code.
        /// </summary>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }
}
