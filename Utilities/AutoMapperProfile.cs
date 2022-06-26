using AutoMapper;
using BusBoiBackend.Models;
using BusBoiBackend.OneBusAway.IncomingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RouteDTO, Route>();
            CreateMap<StopDTO, Stop>();
            CreateMap<ArrivalsAndDeparturesForStopDTO, WatchedBus>()
                .ForMember(f => f.Latitude, o => o.MapFrom(s => s.TripStatus.Position.Lat))
                .ForMember(f => f.Longitude, o => o.MapFrom(s => s.TripStatus.Position.Lon))
                .ForMember(f => f.Status, o => o.MapFrom(s => s.TripStatus.Status));
        }
    }
}
