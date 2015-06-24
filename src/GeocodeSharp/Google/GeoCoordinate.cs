using Newtonsoft.Json;

namespace GeocodeSharp.Google
{
    public class GeoCoordinate
    {
        [JsonProperty("lat")]
        public decimal Latitude { get; set; }

        [JsonProperty("lng")]
        public decimal Longitude { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", this.Latitude, this.Longitude);
        }
    }
}
