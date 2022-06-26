using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<WatchedRouteStop> WatchedRouteStops { get; set; } = new List<WatchedRouteStop>();
    }
}
