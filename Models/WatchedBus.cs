using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.Models
{
    public class WatchedBus
    {
        public string VehicleId { get; set; }

        public string TripId { get; set; }

        public string ServiceDate { get; set; }

        public string Status { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public long DistanceFromStop { get; set; }

        public int NumberOfStopsAway { get; set; }

        public long PredictedArrivalTime { get; set; }

        public long PredictedDepartureTime { get; set; }

        public long ScheduledArrivalTime { get; set; }

        public long ScheduledDepartureTime { get; set; }
    }
}
