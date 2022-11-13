using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Back_End.Models.Users___Dto;
using Back_End.Models.Users___Dto.Users___Persons;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Employees___Dto;
using Entities.DataTransferObjects.Estates___Dto;
using Entities.DataTransferObjects.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Volunteers__Dto;
using Entities.Helpers;

namespace Back_End.Profiles
{
    public class UsersProfiles : Profile
    {
        public UsersProfiles()
        {
            //Creo Las clases a ser mapeadas


            CreateMap<Users, EmployeesUsersDto>()

                 .ForMember(dest => dest.RoleName,
                                 opt => opt.MapFrom(src => src.Roles.RoleName))

                    .ForMember(dest => dest.Name,
                                 opt => opt.MapFrom(src => src.Persons.FirstName + " " + src.Persons.LastName))



                       .ForMember(dest => dest.Status,
                                 opt => opt.MapFrom(src => src.Persons.Status));

            CreateMap<Users, VolunteersUsersAppDto>();


            CreateMap<Users, UserEmployeeAuthDto>()
           //.ForPath(i => i.Persons.Birthdate, opt => opt.MapFrom(src => DateTimeOffsetExtensions.GetDate(src.Persons.Birthdate)))

           .ForMember(dest => dest.token,
                              opt => opt.MapFrom(src => UserSecurity.GenerateAccessToken(src.UserID, src.Roles.RoleName)))



           .ForMember(dest => dest.RoleName,
                              opt => opt.MapFrom(src => src.Roles.RoleName));




            CreateMap<Users, UsersVehiclesDto>();

            CreateMap<Users, EmployeeUserDto>()

                 .ForMember(i => i.Createdate, opt => opt.MapFrom(src => DateTimeOffsetExtensions.GetDateTime(src.CreatedDate)))

                 .ForMember(dest => dest.RoleName,
                              opt => opt.MapFrom(src => src.Roles.RoleName));




            CreateMap<UsersEmployeesForCreationDto, Users>();
            CreateMap<Users, UsersEmployeesForCreationDto>();



            CreateMap<UsersVolunteersForCreationDto, Users>();


            CreateMap<UsersForUpdateDto, Users>();

            CreateMap<Users, UsersForUpdateDto>();
        


            CreateMap<DeviceForUpdateDto, Users>();
            CreateMap<Users, DeviceForUpdateDto>();
        }
    }
}



