using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.OneBusAway.IncomingDTOs
{
    public class RouteDTO
    {
        public int AgencyId { get; set; }
        public string Description { get; set; }
        [JsonProperty("id")]
        public string RouteId { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public int Type { get; set; }
        public string Url { get; set; }
    }
}
