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
    /// Geocode Request parameters
    /// </summary>
    [DebuggerDisplay("address = {address}, components = {components}, bounds = {bounds}, language = {language}, region = {region}")]
    public class GeocodeRequest
    {
        /// <summary>
        /// The street address that you want to geocode, in the format used by the national postal service of the country concerned. Additional address elements such as business names and unit, suite or floor numbers should be avoided. Please refer to the FAQ for additional guidance. 
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// A component filter for which you wish to obtain a geocode. See Component Filtering for more information. The components filter will also be accepted as an optional parameter if an address is provided.
        /// </summary>
        public GeocodeComponent components { get; set; }

        /// <summary>
        /// The bounding box of the viewport within which to bias geocode results more prominently. This parameter will only influence, not fully restrict, results from the geocoder. (For more information see Viewport Biasing below.)
        /// </summary>
        public GeoViewport bounds { get; set; }

        /// <summary>
        /// The language in which to return results. See the list of supported domain languages. Note that we often update supported languages so this list may not be exhaustive. If language is not supplied, the geocoder will attempt to use the native language of the domain from which the request is sent wherever possible.
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// The region code, specified as a ccTLD ("top-level domain") two-character value. This parameter will only influence, not fully restrict, results from the geocoder. (For more information see Region Biasing below.)
        /// </summary>
        public string region { get; set; }

        internal NameValueCollection ToNameValueCollection()
        {
            NameValueCollection nvc = new NameValueCollection();
            if (!string.IsNullOrEmpty(address))
            {
                nvc.Add("address", address);
            }
            else
            {
                throw new ArgumentException("address must be specified");
            }

            if (components != GeocodeComponent.unknown)
            {
                nvc.Add("components",
                    string.Join("|", 
                    Enum.GetValues(components.GetType())
                    .Cast<Enum>()
                    .Where(value=>components.HasFlag(value) && ((GeocodeComponent)value) != GeocodeComponent.unknown)
                    .Select(i=>EnumUtils.GetValue( i))));
            }

            if (bounds != null && bounds.Northeast != null && bounds.Southwest != null)
            {
                nvc.Add("bounds", string.Format("{0},{1}|{2},{3}", bounds.Northeast.Latitude, bounds.Northeast.Longitude, bounds.Southwest.Latitude, bounds.Southwest.Longitude));
            }

            if (!string.IsNullOrEmpty(language))
            {
                nvc.Add("language", language);
            }

            if (!string.IsNullOrEmpty(region))
            {
                nvc.Add("region", region);
            }

            return nvc;
        }
    }
}
