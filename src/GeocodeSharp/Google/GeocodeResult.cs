using System.Diagnostics;
using Newtonsoft.Json;

namespace GeocodeSharp.Google
{
    [DebuggerDisplay("AddressComponents = {string,Join(\",\",AddressComponents)}, FormattedAddress = {FormattedAddress}, Geometry = {Geometry}, PartialMatch = {PartialMatch}, PlaceId = {PlaceId}, PostcodeLocalities = {PostcodeLocalities}, Types = {Types}")]
    public class GeocodeResult
    {
        /// <summary>
        /// an array containing the separate address components
        /// </summary>
        [JsonProperty("address_components")]
        public AddressComponent[] AddressComponents { get; set; }

        /// <summary>
        ///  is a string containing the human-readable address of this location. Often 
        ///  this address is equivalent to the "postal address," which sometimes differs 
        ///  from country to country. (Note that some countries, such as the United Kingdom, 
        ///  do not allow distribution of true postal addresses due to licensing restrictions.) 
        ///  This address is generally composed of one or more address components. 
        ///  For example, the address "111 8th Avenue, New York, NY" contains 
        ///  separate address components for "111" (the street number, "8th Avenue" (the route)
        ///  "New York" (the city) and "NY" (the US state). 
        ///  These address components contain additional information as noted below
        /// </summary>
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        /// <summary>
        /// indicates that the geocoder did not return an exact match for the original request, though 
        /// it was able to match part of the requested address. You may wish to examine the original 
        /// request for misspellings and/or an incomplete address.
        /// <para>
        /// Partial matches most often occur for street addresses that do not exist within the locality 
        /// you pass in the request. Partial matches may also be returned when a request matches 
        /// two or more locations in the same locality. For example, "21 Henr St, Bristol, UK" will 
        /// return a partial match for both Henry Street and Henrietta Street. Note that if a request 
        /// includes a misspelled address component, the geocoding service may suggest an alternative address. 
        /// Suggestions triggered in this way will not be marked as a partial match.
        /// </para>
        /// </summary>
        [JsonProperty("partial_match")]
        public bool PartialMatch { get; set; }

        /// <summary>
        /// place_id is a unique identifier that can be used with other Google APIs. For example, you can use the place_id in a Google Places API request to get details of a local business, such as phone number, opening hours, user reviews, and more. See the place ID overview.
        /// </summary>
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        /// <summary>
        ///  an array denoting all the localities contained in a postal code. This is only present when the result is a postal code that contains multiple localities.
        /// </summary>
        [JsonProperty("postcode_localities")]
        public string[] PostcodeLocalities { get; set; }

        /// <summary>
        /// This array contains a set of zero or more tags identifying the type of feature returned 
        /// in the result. For example, a geocode of "Chicago" returns "locality" which indicates 
        /// that "Chicago" is a city, and also returns "political" which indicates it is a political entity.
        /// </summary>
        [JsonProperty("types")]
        public string[] Types { get; set; }
    }
}
