using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics;

namespace GeocodeSharp.Google
{
    /// <summary>
    /// Encapsulates methods for executing geocode requests.
    /// </summary>
    [DebuggerDisplay("apikey = {_apiKey}")]
    public class GeocodeClient 
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;

        /// <summary>
        /// Initialize GeocodeClient without a Google API key and use default annonymouse access.
        /// NOTE: Throttling may apply.
        /// </summary>
        public GeocodeClient()
        {
            _baseUrl = "http://maps.googleapis.com/maps/api/geocode/json?";
        }

        /// <summary>
        /// Initialize GeocodeClient with your Google API key to utilize it in the requests to Google and bypass the default annonymous throttling.
        /// </summary>
        /// <param name="apiKey">Google Maps API Key</param>
        public GeocodeClient(string apiKey) : this()
        {
            if (!string.IsNullOrEmpty(apiKey))
            {
                _apiKey = apiKey;
                _baseUrl = string.Format("https://maps.googleapis.com/maps/api/geocode/json?key={0}&", Uri.EscapeDataString(_apiKey));
            }
        }

        /// <summary>
        /// Calls Google's geocode API with the specified address and optional region.
        /// https://developers.google.com/maps/documentation/geocoding/#GeocodingRequests
        /// </summary>
        /// <param name="address">The street address that you want to geocode, in the format used by the national postal service of the country concerned. Additional address elements such as business names and unit, suite or floor numbers should be avoided.</param>
        /// <param name="region">The region code, specified as a ccTLD ("top-level domain") two-character value. This parameter will only influence, not fully restrict, results from the geocoder.</param>
        /// <returns>The geocode response.</returns>
        public async Task<GeocodeResponse> GeocodeAddress(string address, string region = null)
        {
            var nvc = new NameValueCollection();
            if (!string.IsNullOrEmpty(address))
            {
                nvc.Add("address",address);
            }
            if (!string.IsNullOrEmpty(region))
            {
                nvc.Add("region", region);
            }
            return await GeocodeRequest(nvc);
        }

        /// <summary>
        ///  Calls Google's geocode API with specific request parameters
        /// </summary>
        /// <returns>The geocode response.</returns>
        public async Task<GeocodeResponse> GeocodeRequest(GeocodeRequest request)
        {
            return await GeocodeRequest(request.ToNameValueCollection());
        }

        /// <summary>
        ///  Calls Google's geocode reverse API with specific request parameters
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GeocodeResponse> GeocodeRequest(GeocodeReverseRequest request)
        {
            return await GeocodeRequest(request.ToNameValueCollection());
        }

        public async Task<GeocodeResponse> GeocodeRequest(NameValueCollection nvc)
        {
            var url = BuildGeocodeUrl(nvc);

            string json;
            var request = WebRequest.CreateHttp(url);
            using (var ms = new MemoryStream())
            {
                using (var response = await request.GetResponseAsync())
                using (var body = response.GetResponseStream())
                {
                    if (body != null) await body.CopyToAsync(ms);
                }

                json = Encoding.UTF8.GetString(ms.ToArray());
            }
            return JsonConvert.DeserializeObject<GeocodeResponse>(json);
        }

        /// <summary>
        /// The term geocoding generally refers to translating a human-readable address into a location on a map. The process of doing the opposite, translating a location on the map into a human-readable address, is known as reverse geocoding.
        /// </summary>
        /// <param name="latlng">The latitude and longitude values specifying the location for which you wish to obtain the closest, human-readable address.</param>
        /// <returns>The geocode response.</returns>
        public async Task<GeocodeResponse> GeocodeReverse(GeoCoordinate latlng)
        {
            var nvc = new NameValueCollection {{"latlng",latlng.ToString()}};
            return await GeocodeRequest(nvc);
        }

        /// <summary>
        /// The term geocoding generally refers to translating a human-readable address into a location on a map. The process of doing the opposite, translating a location on the map into a human-readable address, is known as reverse geocoding.
        /// </summary>
        /// <param name="placeid">The place ID of the place for which you wish to obtain the human-readable address. The place ID is a unique identifier that can be used with other Google APIs. For example, you can use the placeID returned by the Google Maps Roads API to get the address for a snapped point. For more information about place IDs, see the place ID overview.</param>
        /// <returns>The geocode response.</returns>
        public async Task<GeocodeResponse> GeocodeReverse(string placeid)
        {
            var nvc = new NameValueCollection { { "place_id",placeid} };
            return await GeocodeRequest(nvc);
        }

        private string BuildGeocodeUrl(NameValueCollection nvcParameters)
        {
            if (nvcParameters.Count == 0)
            {
                throw new ArgumentNullException();
            }

            UriBuilder uri = new UriBuilder(_baseUrl);
            
            NameValueCollection nvc = new NameValueCollection(nvcParameters);

            if (uri.Query != null)
            {
                foreach (var uparam in uri.Query.Trim(new char[] { '?', ' ', '&' }).Split('&'))
                {
                    if (uparam == "")
                    {
                        continue;
                    }

                    var usplit = uparam.Split('=');
                    nvc.Set(usplit[0]
                        , WebUtility.UrlDecode(usplit[1]));
                }
            }
            
            uri.Query = string.Join("&", 
                nvc.AllKeys
                .Where(key=>nvc.GetValues(key).Count() > 0 && nvc.GetValues(key).All(i=>!string.IsNullOrWhiteSpace(i))) 
                .Select(key=>string.Format("{0}={1}", key,string.Join(",",nvc.GetValues(key))))
                .ToArray());

            return uri.ToString();
        }
    }
}
