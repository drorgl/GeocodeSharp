using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocodeSharp.Google
{
    /// <summary>
    /// Reverse Geocode Request Parameters
    /// </summary>
    [DebuggerDisplay("latlng = {latlng}, placeid = {placeid}, language = {language}, result_type = {result_type}, location_type = {location_type}")]
    public class GeocodeReverseRequest
    {
        /// <summary>
        /// The latitude and longitude values specifying the location for which you wish to obtain the closest, human-readable address.
        /// </summary>
        public GeoCoordinate latlng { get; set; }

        /// <summary>
        /// The place ID of the place for which you wish to obtain the human-readable address. The place ID is a unique identifier that can be used with other Google APIs. For example, you can use the placeID returned by the Google Maps Roads API to get the address for a snapped point. For more information about place IDs, see the place ID overview.
        /// </summary>
        public string placeid { get; set; }

        /// <summary>
        /// The language in which to return results. See the list of supported domain languages. Note that we often update supported languages so this list may not be exhaustive. If language is not supplied, the geocoder will attempt to use the native language of the domain from which the request is sent wherever possible.
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// One or more address types, separated by a pipe (|). Examples of address types: country, street_address, postal_code. For a full list of allowable values, see the address types on this page. Specifying a type will restrict the results to this type. If multiple types are specified, the API will return all addresses that match any of the types. Note: This parameter is available only for requests that include an API key or a client ID.
        /// </summary>
        public GeocodeComponent result_type { get; set; }

        /// <summary>
        /// one or more location types, separated by a pipe (|). Specifying a type will restrict the results to this type. If multiple types are specified, the API will return all addresses that match any of the types. 
        /// <para>
        /// Note: This parameter is available only for requests that include an API key or a client ID. 
        /// </para>
        /// </summary>
        public LocationType location_type { get; set; }

        internal NameValueCollection ToNameValueCollection()
        {
            NameValueCollection nvc = new NameValueCollection();

            if (latlng != null)
            {
                nvc.Add("latlng", latlng.ToString());
            }

            if (!string.IsNullOrEmpty(placeid))
            {
                nvc.Add("place_id", placeid);
            }

            if (result_type != GeocodeComponent.unknown)
            {
                nvc.Add("result_type",
                    string.Join("|",
                    Enum.GetValues(result_type.GetType())
                    .Cast<Enum>()
                    .Where(value => result_type.HasFlag(value) && ((GeocodeComponent)value) != GeocodeComponent.unknown)
                    .Select(i => EnumUtils.GetValue(i))));
            }

            if (!string.IsNullOrEmpty(language))
            {
                nvc.Add("language", language);
            }

            if (location_type !=  LocationType.Unknown)
            {
                nvc.Add("location_type",
                    string.Join("|",
                    Enum.GetValues(location_type.GetType())
                    .Cast<Enum>()
                    .Where(value => location_type.HasFlag(value) && ((LocationType)value) != LocationType.Unknown)
                    .Select(i =>EnumUtils.GetValue( i))));
            }

            return nvc;
        }

    }
}
