using BusBoiBackend.Models;
using BusBoiBackend.OneBusAway;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [GraphQLDescription("Represents all available routes from various agencies")]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Route> GetRoute([ScopedService] AppDbContext context)
        {
            return context.Routes;
        }

        [UseDbContext(typeof(AppDbContext))]
        [GraphQLDescription("Represents all available routes from various agencies")]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Stop> GetStop([ScopedService] AppDbContext context)
        {
            return context.Stops;
        }

        [UseDbContext(typeof(AppDbContext))]
        [GraphQLDescription("Represents available stops with a set amount of meters from the given latitude and longitude")]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<Stop>> GetStopsByLatLon([Service] OneBusAwayHandler obaHandler, [ScopedService] AppDbContext context, double lat, double lon)
        {
            var stops = await obaHandler.GetStopsByLatLonAsync(lat, lon);

            List<string> newStopIds = new List<string>();

            foreach (Stop stop in stops)
            {
                newStopIds.Add(stop.StopId);
            }

            return context.Stops.Where(s => newStopIds.Contains(s.StopId));
        }

        [UseDbContext(typeof(AppDbContext))]
        [GraphQLDescription("Represents incoming busses to the route stop combination")]
        public async Task<List<WatchedRouteStop>> GetWatchedBussesForUser([Service] OneBusAwayHandler obaHandler, [ScopedService] AppDbContext context, string username)
        {
            var user = context.Users
                .Include(u => u.WatchedRouteStops)
                .FirstOrDefault(u => u.UserName == username);

            var watchedRouteStops = context.WatchedRouteStops
                .Include(w => w.Route)
                .Include(w => w.Stop)
                .Where(w => w.User == user);

            List<WatchedRouteStop> toReturn = new List<WatchedRouteStop>();

            foreach (WatchedRouteStop localWrs in watchedRouteStops)
            {
                var tempWrs = localWrs;
                tempWrs.WatchedBusses = await obaHandler.GetWatchedBussesForRouteStop(localWrs);
                toReturn.Add(tempWrs);
                // Delay is needed to avoid a 429-"Too Many Requests" status code
                await Task.Delay(50);
            }

            return toReturn;
        }
    }
}
