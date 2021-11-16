using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Back_End.Models.Users___Dto;
using Back_End.Models.Users___Dto.Users___Persons;
using Entities.DataTransferObjects.Login___Dto;
using Entities.DataTransferObjects.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Volunteers__Dto;

namespace Back_End.Profiles
{
    public class UsersProfiles : Profile
    {
        public UsersProfiles()
        {
            //Creo Las clases a ser mapeadas

            CreateMap<Users, EmployeesUsersDto>()
                 .ForMember(dest => dest.RoleName,
                                 opt => opt.MapFrom(src => src.Roles.RoleName));

            /*CreateMap<Users, VolunteersUsersDto>()
                    .ForMember(dest => dest.RoleName,
                                 opt => opt.MapFrom(src => src.Roles.RoleName));*/

            CreateMap<Users, VolunteersUsersAppDto>();

            CreateMap<Users, UserEmployeeAuthDto>()
           .ForMember(dest => dest.token,
                              opt => opt.MapFrom(src => UserSecurity.GenerateAccessToken(src.UserID, src.Roles.RoleName)))

           .ForMember(dest => dest.RoleName,
                              opt => opt.MapFrom(src => src.Roles.RoleName))
           
            .ForMember(dest => dest.VolunteerAvatar,
                              opt => opt.MapFrom(src => src.Volunteers.VolunteerAvatar));

            CreateMap<Users, UsersVehiclesDto>();


            CreateMap<UsersEmployeesForCreationDto, Users>();

            CreateMap<UsersVolunteersForCreationDto, Users>();

            CreateMap<UsersForUpdateDto, Users>();
            CreateMap<Users, UsersForUpdateDto>();
        }
    }
}



