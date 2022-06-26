using BusBoiBackend.GraphQL.Payloads;
using BusBoiBackend.Models;
using HotChocolate;
using HotChocolate.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddUserPayload> AddUserAsync(string userName, string password,
            [ScopedService] AppDbContext context)
        {
            var newUser = new User
            {
                UserName = userName,
                PasswordHash = password
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return new AddUserPayload(newUser);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<WatchedRouteStop> AddWatchedRouteStopAsync(string userName, string routeId, string stopId, string hexColor,
            [ScopedService] AppDbContext context)
        {
            if (context.WatchedRouteStops.Any(w => w.User.UserName == userName && w.Route.RouteId == routeId && w.Stop.StopId == stopId))
            {
                return null;
            }
            // Grab associated data
            var assocRoute = context.Routes.Where(r => r.RouteId == routeId).FirstOrDefault();
            var assocStop = context.Stops.Where(s => s.StopId == stopId).FirstOrDefault();
            var assocUser = context.Users.Where(u => u.UserName == userName).FirstOrDefault();

            var newWatchedRouteStop = new WatchedRouteStop
            {
                Route = assocRoute,
                Stop = assocStop,
                User = assocUser,
                Color = hexColor
            };

            context.WatchedRouteStops.Add(newWatchedRouteStop);
            await context.SaveChangesAsync();

            return newWatchedRouteStop;
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<WatchedRouteStop> RemoveWatchedRouteStop(string username, string routeId, string stopId,
            [ScopedService] AppDbContext context)
        {
            var wrs = context.WatchedRouteStops.FirstOrDefault(w => w.User.UserName == username && w.Route.RouteId == routeId && w.Stop.StopId == stopId);
            context.WatchedRouteStops.Remove(wrs);
            await context.SaveChangesAsync();
            return wrs;
        }
    }
}
