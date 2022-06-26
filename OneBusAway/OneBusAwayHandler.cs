using AutoMapper;
using BusBoiBackend.Models;
using BusBoiBackend.OneBusAway.IncomingDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.OneBusAway
{
    public class OneBusAwayHandler
    {
        private readonly ILogger<OneBusAwayHandler> logger;
        private readonly OneBusAwayService obas;
        private readonly IMapper mapper;
        private readonly AppDbContext context;

        public OneBusAwayHandler(ILogger<OneBusAwayHandler> logger, OneBusAwayService obas, IMapper mapper, IDbContextFactory<AppDbContext> contextFactory)
        {
            this.logger = logger;
            this.obas = obas;
            this.mapper = mapper;
            this.context = contextFactory.CreateDbContext();
        }

        public async void SeedAllRoutes()
        {
            // Get Route and Stop Data from One Bus Away API
            var allRoutes = await obas.GetAllRoutesAsync();

            try
            {
                foreach(RouteDTO incomingRoute in allRoutes)
                {
                    var newRoute = mapper.Map<RouteDTO, Route>(incomingRoute);
                    if (!context.Routes.Any(r => r.RouteId == newRoute.RouteId))
                    {
                        context.Routes.Add(newRoute);
                    } else
                    {
                        // TODO: Add logic for updating stop information if the info has changed
                    }
                }

                await context.SaveChangesAsync();

            } catch(Exception ex)
            {
                Console.WriteLine("Debug Line");
            }
        }

        public async Task<List<Stop>> GetStopsByLatLonAsync(double lat, double lon)
        {
            // Get incoming stops
            List<StopDTO> incomingStops = await obas.GetStopsByLatLonAsync(lat, lon);

            List<Stop> newStops = new List<Stop>();

            try
            {
                foreach (StopDTO incomingStop in incomingStops)
                {
                    Stop newStop = mapper.Map<StopDTO, Stop>(incomingStop);
                    // Get assoc routes
                    List<Route> assocRoutes = new List<Route>();
                    foreach (string routeId in incomingStop.RouteIds)
                    {
                        var route = context.Routes.Where(r => r.RouteId == routeId).FirstOrDefault();
                        if (route != null)
                        {
                            assocRoutes.Add(route);
                        }
                    }
                    newStop.Routes = assocRoutes;
                
                    if (!context.Stops.Any(s => s.StopId == newStop.StopId))
                    {
                        context.Stops.Add(newStop);
                    } else
                    {
                        // TODO: Add logic for updating stop information if the info has changed
                    }
                    newStops.Add(newStop);
                }

                await context.SaveChangesAsync();
            } catch (Exception ex)
            {
                Console.WriteLine("Debug Line");
            }

            return newStops;
        }

        public async Task<List<WatchedBus>> GetWatchedBussesForRouteStop(WatchedRouteStop wrs)
        {
            List<ArrivalsAndDeparturesForStopDTO> incomingArrivalsAndDepartures = await obas.GetArrivalsAndDeparturesForStopRoute(wrs.Stop.StopId, wrs.Route.RouteId);
            List<WatchedBus> newWatchedBusses = new List<WatchedBus>();

            DateTimeOffset now = DateTimeOffset.UtcNow;
            long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();

            foreach (var entry in incomingArrivalsAndDepartures)
            {
                if (long.Parse(entry.ScheduledArrivalTime) > unixTimeMilliseconds)
                {
                    newWatchedBusses.Add(mapper.Map<ArrivalsAndDeparturesForStopDTO, WatchedBus>(entry));
                }
            }

            return newWatchedBusses;
        }
    }
}
