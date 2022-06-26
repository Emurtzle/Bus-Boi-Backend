using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.OneBusAway.IncomingDTOs
{
    public class StopDTO
    {
        public string Direction { get; set; }
        [JsonProperty("id")]
        public string StopId { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        public string Name { get; set; }
        public List<string> RouteIds { get; set; }
    }
}
