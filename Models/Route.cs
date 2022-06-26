using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.Models
{
    public class Route
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RouteId { get; set; }

        [Required]
        public int AgencyId { get; set; }

        public string Description { get; set; }

        public string LongName { get; set; }

        public string ShortName { get; set; }

        public int Type { get; set; }

        public string Url { get; set; }

        public ICollection<Stop> Stops { get; set; } = new List<Stop>();
    }
}
