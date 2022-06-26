using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.OneBusAway.IncomingDTOs.ResponseWrappers
{
    public class ArrivalsAndDeparturesForStopWrapper
    {
        [JsonProperty("data")]
        public ArrivalsAndDeparturesForStopDataProp Data { get; set; }
    }

    public class ArrivalsAndDeparturesForStopDataProp
    {
        [JsonProperty("entry")]
        public ArrivalsAndDeparturesForStopEntryProp Entry { get; set; }
    }

    public class ArrivalsAndDeparturesForStopEntryProp
    {
        [JsonProperty("arrivalsAndDepartures")]
        public List<ArrivalsAndDeparturesForStopDTO> ArrivalsAndDepartures { get; set; }
    }
}
