using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.OneBusAway.IncomingDTOs
{
    public class ArrivalsAndDeparturesForStopDTO
    {
        public string VehicleId { get; set; }
        public string TripId { get; set; }
        public string RouteId { get; set; }
        public string ServiceDate { get; set; }
        public int NumberOfStopsAWay { get; set; }
        public string PredictedArrivalTime { get; set; }
        public string PredictedDepartureTime { get; set; }
        public string ScheduledArrivalTime { get; set; }
        public string ScheduledDepartureTime { get; set; }
        public long DistanceFromStop { get; set; }
        public TripStatus TripStatus { get; set; }
    }

    public class TripStatus
    {
        public Position Position { get; set; }
        public string NextStop { get; set; }
        public string Status { get; set; }
    }

    public class Position
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
}
