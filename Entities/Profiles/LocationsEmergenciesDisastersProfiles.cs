using AutoMapper;
using Entities.DataTransferObjects.LocationsEmergenciesDisasters___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class LocationsEmergenciesDisastersProfiles : Profile
    {
        public LocationsEmergenciesDisastersProfiles()
        {
            CreateMap<LocationsEmergenciesDisasters, LocationsEmergenciesDisastersDto>();

            CreateMap<LocationsEmergenciesDisasters, LocationsEmergenciesDisastersDto>();

            CreateMap<LocationsEmergenciesDisastersDto, LocationsEmergenciesDisasters>();
        }
    }
}
