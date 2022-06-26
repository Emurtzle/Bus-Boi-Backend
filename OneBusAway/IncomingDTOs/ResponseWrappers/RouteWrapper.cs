using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.OneBusAway.IncomingDTOs.ResponseWrappers
{
    public class RouteWrapper
    {
        [JsonProperty("data")]
        public RouteDataProp Data { get; set; }
    }

    public class RouteDataProp
    {
        [JsonProperty("entry")]
        public RouteDTO Entry { get; set; }
    }
}
