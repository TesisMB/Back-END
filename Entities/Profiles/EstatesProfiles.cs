using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.Estates___Dto;
using Entities.DataTransferObjects.Models.Vehicles___Dto;
using Entities.DataTransferObjects.ResourcesDto;

namespace Back_End.Profiles
{
    public class EstatesProfiles : Profile
    {
        public EstatesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Estates, EstatesDto>()

                 .ForPath(resp => resp.LocationAddressID, opt => opt.MapFrom(src => src.LocationAddress.LocationAddressID))
                 
                 .ForPath(resp => resp.LocationsID, opt => opt.MapFrom(src => src.Locations.LocationID))

                .ForPath(resp => resp.Address, opt => opt.MapFrom(src => $"{src.LocationAddress.Address} {src.LocationAddress.NumberAddress}"))

               .ForPath(resp => resp.PostalCode, opt => opt.MapFrom(src => src.LocationAddress.PostalCode));


            CreateMap<Estates, EstateDto>()
                                .ForPath(resp => resp.Address, opt => opt.MapFrom(src => $"{src.LocationAddress.Address} {src.LocationAddress.NumberAddress}"));


            CreateMap<Estates, EstatesVehiclesDto>();

            CreateMap<Estates, EstatesTypeDto>();

        

       
;

        }
    }
}
