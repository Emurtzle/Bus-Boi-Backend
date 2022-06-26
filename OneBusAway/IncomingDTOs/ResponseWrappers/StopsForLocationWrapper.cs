using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.OneBusAway.IncomingDTOs.ResponseWrappers
{
    public class StopsForLocationWrapper
    {
        [JsonProperty("data")]
        public StopsForLocationDataProp Data { get; set; }
    }

    public class StopsForLocationDataProp
    {
        [JsonProperty("limitExceeded")]
        public bool LimitExceeded { get; set; }
        
        [JsonProperty("list")]
        public List<StopDTO> List { get; set; }
    }
}
