using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.Models
{
    public class WatchedRouteStop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Route Route { get; set; }

        [Required]
        public Stop Stop { get; set; }

        [Required]
        public string Color { get; set; }

        [NotMapped]
        public List<WatchedBus> WatchedBusses { get; set; }
    }
}
