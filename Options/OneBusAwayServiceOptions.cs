using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoi_Backend.Data.Options
{
    public class OneBusAwayServiceOptions
    {
        public const string Section = "OneBusAway";
        public string Key { get; set; }
        public int StopSearchRadius { get; set; }
        public int RouteSearchRadius { get; set; }
    }
}
