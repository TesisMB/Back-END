using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.Estates___Dto;
using Entities.DataTransferObjects.Models.Vehicles___Dto;

namespace Back_End.Profiles
{
    public class EstatesProfiles : Profile
    {
        public EstatesProfiles()
        {
            //Creo Las clases a ser mapeadas




            CreateMap<Estates, EstateDto>()

                                      .ForPath(resp => resp.LocationCityName, opt => opt.MapFrom(src => src.Locations.LocationCityName))
                                      .ForPath(resp => resp.LocationAddress, opt => opt.MapFrom(src => src.LocationAddress));

            // .ForPath(resp => resp.Address, opt => opt.MapFrom(src => $"{src.LocationAddress.Address} {src.LocationAddress.NumberAddress}"));


            CreateMap<Estates, EstatesVehiclesDto>();

            CreateMap<Estates, EstatesDto>()
                              .ForPath(resp => resp.LocationCityName, opt => opt.MapFrom(src => src.Locations.LocationCityName))

                              .ForPath(resp => resp.locationID, opt => opt.MapFrom(src => src.Locations.LocationID))

                              .ForPath(resp => resp.PostalCode, opt => opt.MapFrom(src => src.LocationAddress.PostalCode))

                              .ForPath(resp => resp.Address, opt => opt.MapFrom(src => src.LocationAddress.Address + " " + src.LocationAddress.NumberAddress));
;

            CreateMap<Estates, EstatesLoginDto>()
                                 .ForPath(resp => resp.locationCityName, opt => opt.MapFrom(src => src.Locations.LocationCityName))
;




            ;

        }
    }
}
