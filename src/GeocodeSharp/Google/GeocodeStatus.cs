using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace GeocodeSharp.Google
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GeocodeStatus
    {
        /// <summary>
        ///  indicates that no errors occurred; the address was successfully parsed and at least one geocode was returned.
        /// </summary>
        [EnumMember(Value = "OK")]
        Ok,
        /// <summary>
        ///  indicates that the geocode was successful but returned no results. This may occur if the geocoder was passed a non-existent address.
        /// </summary>
        [EnumMember(Value = "ZERO_RESULTS")]
        ZeroResults,
        /// <summary>
        /// indicates that you are over your quota.
        /// </summary>
        [EnumMember(Value = "OVER_QUERY_LIMIT")]
        OverQueryLimit,
        /// <summary>
        /// indicates that your request was denied.
        /// </summary>
        [EnumMember(Value = "REQUEST_DENIED")]
        RequestDenied,
        /// <summary>
        /// generally indicates that the query (address, components or latlng) is missing.
        /// </summary>
        [EnumMember(Value = "INVALID_REQUEST")]
        InvalidRequest,
        /// <summary>
        ///  indicates that the request could not be processed due to a server error. The request may succeed if you try again.
        /// </summary>
        [EnumMember(Value = "UNKNOWN_ERROR")]
        UnknownError,

        Unexpected, // in case the server returns an un-expected value
    }
}
