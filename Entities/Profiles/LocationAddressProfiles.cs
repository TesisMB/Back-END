using AutoMapper;
using Back_End.Models;
using Entities.DataTransferObjects.Models.Vehicles___Dto;

namespace Back_End.Profiles
{
    public class LocationAddressProfiles : Profile
    {
        public LocationAddressProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<LocationAddress, LocationAddressDto>()
                
                .ForMember(a => a.Address, src => src.MapFrom(a => a.Address + " " + a.NumberAddress));

            CreateMap<LocationAddress, LocationAddressVehiclesDto>();
        }
    }
}
