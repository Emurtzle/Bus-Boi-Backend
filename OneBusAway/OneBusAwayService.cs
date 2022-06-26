using BusBoi_Backend.Data.Options;
using BusBoiBackend.OneBusAway.IncomingDTOs;
using BusBoiBackend.OneBusAway.IncomingDTOs.ResponseWrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusBoiBackend.OneBusAway
{
    public class OneBusAwayService
    {
        private readonly IConfiguration configuration;

        public HttpClient Client { get; }
        private string Key;
        private int StopSearchRadius;
        private int RouteSearchRadius;

        public OneBusAwayService(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri("http://api.pugetsound.onebusaway.org/api/where/");
            Client = client;
            this.configuration = configuration;
            this.Key = this.configuration.GetConnectionString("OBA_Key");
            this.StopSearchRadius = this.configuration.GetValue<int>("OBA_StopSearchRadius");
            this.RouteSearchRadius = this.configuration.GetValue<int>("OBA_RouteSearchRadius");
        }

        public async Task<List<RouteDTO>> GetAllRoutesAsync()
        {
            // 1 -> Metro Transit
            // 3 -> Pierce Transit
            // 19 -> Intercity Transit
            // 23 -> City of Seattle
            // 29 -> Community transit
            // 40 -> Sound Transit
            // 95 -> Washington State Ferries
            // 96 -> Seattle Monorail
            // 97 -> Everett Transit
            // 98 -> Seattle Childrens Hospital

            List<int> agencies = new List<int> { 1, 23, 40, 96 };
            List<RouteDTO> allRoutes = new List<RouteDTO>();

            foreach (int agency in agencies)
            {
                string url = $"routes-for-agency/{ agency }.json?key={ Key }";
                using (HttpResponseMessage response = await Client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Uses newtonsoft json converter to map json over to incoming route model
                        string jsonResp = await response.Content.ReadAsStringAsync();
                        RoutesByAgencyWrapper obaResp = JsonConvert.DeserializeObject<RoutesByAgencyWrapper>(jsonResp);
                        allRoutes.AddRange(obaResp.Data.List);
                    }
                    else
                    {
                        // TODO: Add exceptions to an error array to pass back
                        // TODO: Should retry operation first, Its only a free tier api key
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                // Delay is needed to avoid a 429-"Too Many Requests" status code
                await Task.Delay(2000);
            }
            return allRoutes;
        }

        public async Task<List<StopDTO>> GetStopsByLatLonAsync(double lat, double lon)
        {
            List<StopDTO> stops = new List<StopDTO>();
            int radius = StopSearchRadius;
            string url = $"stops-for-location.json?key={ Key }&lat={ lat }&lon={ lon }&radius={ radius }";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string respJson = await response.Content.ReadAsStringAsync();
                    StopsForLocationWrapper obaResp = JsonConvert.DeserializeObject<StopsForLocationWrapper>(respJson);
                    stops.AddRange(obaResp.Data.List);
                }
                else
                {
                    // TODO: Handle exceptions
                    throw new Exception(response.ReasonPhrase);
                }
            }
            return stops;
        }

        public async Task<List<RouteDTO>> GetRouteListAsync(List<string> routeIds)
        {
            List<RouteDTO> routes = new List<RouteDTO>();
            foreach (string routeId in routeIds)
            {
                string url = $"route/{ routeId }.json?key={ Key }";
                using (HttpResponseMessage response = await Client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string respJson = await response.Content.ReadAsStringAsync();
                        RouteWrapper obaResp = JsonConvert.DeserializeObject<RouteWrapper>(respJson);
                        routes.Add(obaResp.Data.Entry);

                    } else
                    {
                        // TODO: Handle exceptions
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            return routes;
        }



        public async Task<List<ArrivalsAndDeparturesForStopDTO>> GetArrivalsAndDeparturesForStopRoute(string stopId, string routeId)
        {
            List<ArrivalsAndDeparturesForStopDTO> arrivalsAndDepartures = new List<ArrivalsAndDeparturesForStopDTO>();
            string url = $"arrivals-and-departures-for-stop/{stopId}.json?key={ Key }";

            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string respJson = await response.Content.ReadAsStringAsync();
                    ArrivalsAndDeparturesForStopWrapper obaResp = JsonConvert.DeserializeObject<ArrivalsAndDeparturesForStopWrapper>(respJson);

                    foreach (var entry in obaResp.Data.Entry.ArrivalsAndDepartures)
                    {
                        if (entry.RouteId == routeId)
                        {
                            arrivalsAndDepartures.Add(entry);
                        }
                    }
                }
                else
                {
                    // TODO: Handle Exceptions
                    throw new Exception(response.ReasonPhrase);
                }
            }
            return arrivalsAndDepartures;
        }




    }
}
