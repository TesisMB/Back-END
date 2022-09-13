using AutoMapper;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class EmergenciesDisastersProfiles : Profile
    {
        public EmergenciesDisastersProfiles()
        {
            CreateMap<EmergenciesDisasters, EmergenciesDisastersDto>()

                .ForMember(dest => dest.EmployeeName,
                                opt => opt.MapFrom(src => src.EmployeeIncharge.Users.Persons.FirstName + " " + src.EmployeeIncharge.Users.Persons.LastName))


                .ForMember(dest => dest.CreatedByEmployee,
                                opt => opt.MapFrom(src => src.EmployeeCreated.Users.Persons.FirstName + " " + src.EmployeeCreated.Users.Persons.LastName))


                    .ForMember(dest => dest.ModifiedByEmployee,
                                opt => opt.MapFrom(src => src.EmployeeModified.Users.Persons.FirstName + " " + src.EmployeeModified.Users.Persons.LastName))
                    //TO-DO CORREGIR MODELO
                  //.ForPath(dest => dest.UsersChatRooms, opt => opt.MapFrom(src => src.ChatRooms.UsersChatRooms));

            .ForPath(dest => dest.UsersChatRooms,
                                opt => opt.MapFrom(src => src.ChatRooms.UsersChatRooms));


            CreateMap<EmergenciesDisasters, EmergenciesDisastersSelectDto>();

            CreateMap<EmergenciesDisasters, EmergenciesDisastersAppDto>()
                          .ForPath(dest => dest.UsersChatRooms,
                                opt => opt.MapFrom(src => src.ChatRooms.UsersChatRooms));


            CreateMap<EmergenciesDisastersForCreationDto, EmergenciesDisasters>();




            CreateMap<EmergenciesDisastersForUpdateDto, EmergenciesDisasters>();

            CreateMap<EmergenciesDisasters, EmergenciesDisastersForUpdateDto>();

            CreateMap<EmergenciesDisasters, ChatRoomsEmergenciesDisastersDto>();

            CreateMap<EmergenciesDisasters, ChatRoomsEmergenciesDisastersDto>();



            /* .ForPath(resp => resp.LocationCityName, op => op.MapFrom(a => a.Locations.LocationCityName))

             .ForPath(resp => resp.TypeEmergencyDisasterID, op => op.MapFrom(a => a.TypesEmergenciesDisasters.TypeEmergencyDisasterID))

             .ForPath(resp => resp.TypeEmergencyDisasterIcon, op => op.MapFrom(a => a.TypesEmergenciesDisasters.TypeEmergencyDisasterIcon))

             .ForPath(resp => resp.TypeEmergencyDisasterName, op => op.MapFrom(a => a.TypesEmergenciesDisasters.TypeEmergencyDisasterName));*/

        }
    }
}
