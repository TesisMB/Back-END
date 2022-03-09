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
                 .ForPath(resp => resp.Address, opt => opt.MapFrom(src => src.LocationAddress.Address))
                 .ForPath(resp => resp.NumberAddress, opt => opt.MapFrom(src => src.LocationAddress.NumberAddress));
            // .ForPath(resp => resp.Address, opt => opt.MapFrom(src => $"{src.LocationAddress.Address} {src.LocationAddress.NumberAddress}"));


            CreateMap<Estates, EstatesVehiclesDto>();

            CreateMap<Estates, EstatesDto>();




            ;

        }
    }
}
