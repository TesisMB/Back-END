using AutoMapper;
using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.Estates___Dto;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class LocationProfiles : Profile
    {
        public LocationProfiles()
        {
            CreateMap<Locations, LocationsDto>();

            CreateMap<LocationsDto, Locations>();

            CreateMap<Locations, EstatesTypeDto>();

        }
    }
}
