using AutoMapper;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class LocationsVolunteersProfiles : Profile
    {
        public LocationsVolunteersProfiles()
        {
            CreateMap<LocationVolunteers, LocationsVolunteersDto>();
            CreateMap<LocationsVolunteersDto, LocationVolunteers>();
        }
    }
}
