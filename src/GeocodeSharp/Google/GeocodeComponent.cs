using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GeocodeSharp.Google
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GeocodeComponent : long
    {
        unknown = 0,

        /// <summary>
        ///  indicates a precise street address.
        /// </summary>
        [EnumMember(Value = "street_address")]
        street_address = 1 << 0,
        /// <summary>
        ///  indicates a named route (such as "US 101").
        /// </summary>
        [EnumMember(Value = "route")]
        route = 1 << 1,
        /// <summary>
        ///  indicates a major intersection, usually of two major roads.
        /// </summary>
        [EnumMember(Value = "intersection")]
        intersection = 1 << 2,
        /// <summary>
        ///  indicates a political entity. Usually, this type indicates a polygon of some civil administration.
        /// </summary>
        [EnumMember(Value = "political")]
        political = 1 << 3,
        /// <summary>
        /// indicates the national political entity, and is typically the highest order type returned by the Geocoder.
        /// </summary>
        [EnumMember(Value = "country")]
        country = 1 << 4,
        /// <summary>
        ///  indicates a first-order civil entity below the country level. Within the United States, these administrative levels are states. Not all nations exhibit these administrative levels.
        /// </summary>
        [EnumMember(Value = "administrative_area_level_1")]
        administrative_area_level_1 = 1 << 5,
        /// <summary>
        ///  indicates a second-order civil entity below the country level. Within the United States, these administrative levels are counties. Not all nations exhibit these administrative levels.
        /// </summary>
        [EnumMember(Value = "administrative_area_level_2")]
        administrative_area_level_2 = 1 << 6,
        /// <summary>
        ///  indicates a third-order civil entity below the country level. This type indicates a minor civil division. Not all nations exhibit these administrative levels.
        /// </summary>
        [EnumMember(Value = "administrative_area_level_3")]
        administrative_area_level_3 = 1 << 7,
        /// <summary>
        ///  indicates a fourth-order civil entity below the country level. This type indicates a minor civil division. Not all nations exhibit these administrative levels.
        /// </summary>
        [EnumMember(Value = "administrative_area_level_4")]
        administrative_area_level_4 = 1 << 8,
        /// <summary>
        ///  indicates a fifth-order civil entity below the country level. This type indicates a minor civil division. Not all nations exhibit these administrative levels.
        /// </summary>
        [EnumMember(Value = "administrative_area_level_5")]
        administrative_area_level_5 = 1 << 9,
        /// <summary>
        ///  indicates a commonly-used alternative name for the entity.
        /// </summary>
        [EnumMember(Value = "colloquial_area")]
        colloquial_area = 1 << 10,
        /// <summary>
        ///  indicates an incorporated city or town political entity.
        /// </summary>
        [EnumMember(Value = "locality")]
        locality = 1 << 11,
        /// <summary>
        ///  indicates a specific type of Japanese locality, to facilitate distinction between multiple locality components within a Japanese address.
        /// </summary>
        [EnumMember(Value = "ward")]
        ward = 1 << 12,
        /// <summary>
        ///  indicates a first-order civil entity below a locality. 
        ///  For some locations may receive one of the additional types: 
        ///  sublocality_level_1 to sublocality_level_5. Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
        /// </summary>
        [EnumMember(Value = "sublocality")]
        sublocality = 1 << 13,
        /// <summary>
        /// largest geographic sublocality
        /// </summary>
        [EnumMember(Value = "sublocality_level_1")]
        sublocality_level_1 = 1 << 14,
        [EnumMember(Value = "sublocality_level_2")]
        sublocality_level_2 = 1 << 15,
        [EnumMember(Value = "sublocality_level_3")]
        sublocality_level_3 = 1 << 16,
        [EnumMember(Value = "sublocality_level_4")]
        sublocality_level_4 = 1 << 17,
        /// <summary>
        /// smallest geographic sublocality
        /// </summary>
        [EnumMember(Value = "sublocality_level_5")]
        sublocality_level_5 = 1 << 18,
        /// <summary>
        ///  indicates a named neighborhood
        /// </summary>
        [EnumMember(Value = "neighborhood")]
        neighborhood = 1 << 19,
        /// <summary>
        ///  indicates a named location, usually a building or collection of buildings with a common name
        /// </summary>
        [EnumMember(Value = "premise")]
        premise = 1 << 20,
        /// <summary>
        ///  indicates a first-order entity below a named location, usually a singular building within a collection of buildings with a common name
        /// </summary>
        [EnumMember(Value = "subpremise")]
        subpremise = 1 << 21,
        /// <summary>
        ///  indicates a postal code as used to address postal mail within the country.
        /// </summary>
        [EnumMember(Value = "postal_code")]
        postal_code = 1 << 22,
        /// <summary>
        ///  indicates a prominent natural feature.
        /// </summary>
        [EnumMember(Value = "natural_feature")]
        natural_feature = 1 << 23,
        /// <summary>
        ///  indicates an airport.
        /// </summary>
        [EnumMember(Value = "airport")]
        airport = 1 << 24,
        /// <summary>
        ///  indicates a named park.
        /// </summary>
        [EnumMember(Value = "park")]
        park = 1 << 25,
        /// <summary>
        ///  indicates a named point of interest. Typically, these "POI"s are prominent local entities that don't easily fit in another category, such as "Empire State Building" or "Statue of Liberty."
        /// </summary>
        [EnumMember(Value = "point_of_interest")]
        point_of_interest = 1 << 26,

        /// <summary>
        ///  indicates the floor of a building address.
        /// </summary>
        [EnumMember(Value = "floor")]
        floor = 1 << 27,
        /// <summary>
        ///  typically indicates a place that has not yet been categorized.
        /// </summary>
        [EnumMember(Value = "establishment")]
        establishment = 1 << 28,
        /// <summary>
        ///  indicates a parking lot or parking structure.
        /// </summary>
        [EnumMember(Value = "parking")]
        parking = 1 << 29,
        /// <summary>
        ///  indicates a specific postal box.
        /// </summary>
        [EnumMember(Value = "post_box")]
        post_box = 1 << 30,
        /// <summary>
        ///  indicates a grouping of geographic areas, such as locality and sublocality, used for mailing addresses in some countries.
        /// </summary>
        [EnumMember(Value = "postal_town")]
        postal_town = 1 << 31,
        /// <summary>
        ///  indicates the room of a building address.
        /// </summary>
        [EnumMember(Value = "room")]
        room = 1 << 32,
        /// <summary>
        ///  indicates the precise street number.
        /// </summary>
        [EnumMember(Value = "street_number")]
        street_number = 1 << 33,
        /// <summary>
        ///  location of a bus
        /// </summary>
        [EnumMember(Value = "bus_station")]
        bus_station = 1 << 34,
        /// <summary>
        ///  location of a train
        /// </summary>
        [EnumMember(Value = "train_station")]
        train_station = 1 << 35,
        /// <summary>
        ///  location of a public transit stop
        /// </summary>
        [EnumMember(Value = "transit_station")]
        transit_station = 1 << 36,

        /// <summary>
        /// postal code prefix
        /// </summary>
        [EnumMember(Value = "postal_code_prefix")]
        postal_code_prefix = 1 << 37

    }
}
