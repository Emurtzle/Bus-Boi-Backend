using BusBoiBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<Stop> Stops { get; set; }

        public DbSet<WatchedRouteStop> WatchedRouteStops { get; set; }
    }
}
