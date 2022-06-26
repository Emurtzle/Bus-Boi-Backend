using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.OneBusAway.IncomingDTOs.ResponseWrappers
{
    public class RoutesByAgencyWrapper
    {
        [JsonProperty("data")]
        public RoutesByAgencyDataProp Data { get; set; }
    }

    public class RoutesByAgencyDataProp
    {
        [JsonProperty("limitExceeded")]
        public bool LimitExceeded { get; set; }

        [JsonProperty("list")]
        public List<RouteDTO> List { get; set; }
    }
}
