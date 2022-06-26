using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.Models
{
    public class Stop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StopId { get; set; }

        [Required]
        public string Direction { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Route> Routes { get; set; } = new List<Route>();
    }
}
