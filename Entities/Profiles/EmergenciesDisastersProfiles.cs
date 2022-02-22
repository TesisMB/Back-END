using AutoMapper;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.Helpers;
using Entities.Models;

namespace Entities.Profiles
{
    public class EmergenciesDisastersProfiles : Profile
    {
        public EmergenciesDisastersProfiles()
        {
            CreateMap<EmergenciesDisasters, EmergenciesDisastersDto>();


            CreateMap<EmergenciesDisastersForCreationDto, EmergenciesDisasters>();

            CreateMap<EmergenciesDisastersForUpdateDto, EmergenciesDisasters>();

            CreateMap<EmergenciesDisasters, EmergenciesDisastersForUpdateDto>();

            CreateMap<EmergenciesDisasters, ChatRoomsEmergenciesDisastersDto>();

            CreateMap<EmergenciesDisasters, ChatRoomsEmergenciesDisastersDto>()

                .ForPath(src => src.LocationCityName, opt => opt.MapFrom(a => a.Locations.LocationCityName))
                .ForPath(src => src.TypeEmergencyDisasterID, opt => opt.MapFrom(a => a.TypesEmergenciesDisasters.TypeEmergencyDisasterID))
                .ForPath(src => src.TypeEmergencyDisasterName, opt => opt.MapFrom(a => a.TypesEmergenciesDisasters.TypeEmergencyDisasterName))
                .ForPath(src => src.TypeEmergencyDisasterIcon, opt => opt.MapFrom(a => a.TypesEmergenciesDisasters.TypeEmergencyDisasterIcon));



            /* .ForPath(resp => resp.LocationCityName, op => op.MapFrom(a => a.Locations.LocationCityName))

             .ForPath(resp => resp.TypeEmergencyDisasterID, op => op.MapFrom(a => a.TypesEmergenciesDisasters.TypeEmergencyDisasterID))

             .ForPath(resp => resp.TypeEmergencyDisasterIcon, op => op.MapFrom(a => a.TypesEmergenciesDisasters.TypeEmergencyDisasterIcon))

             .ForPath(resp => resp.TypeEmergencyDisasterName, op => op.MapFrom(a => a.TypesEmergenciesDisasters.TypeEmergencyDisasterName));*/

        }
    }
}
