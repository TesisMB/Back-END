using AutoMapper;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class LocationProfiles : Profile
    {
        public LocationProfiles()
        {
            CreateMap<Locations, LocationsDto>();
        }
    }
}
