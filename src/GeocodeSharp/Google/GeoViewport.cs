using System.Diagnostics;
using Newtonsoft.Json;

namespace GeocodeSharp.Google
{
    [DebuggerDisplay("Northeast = {Northeast}, Southwest = {Southwest}")]
    public class GeoViewport
    {
        [JsonProperty("northeast")]
        public GeoCoordinate Northeast { get; set; }

        [JsonProperty("southwest")]
        public GeoCoordinate Southwest { get; set; }
    }
}
