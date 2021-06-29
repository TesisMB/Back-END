using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Back_End.Models.Users___Dto;
using Back_End.Models.Users___Dto.Users___Persons;
using Entities.DataTransferObjects.Models.Vehicles___Dto;

namespace Back_End.Profiles
{
    public class UsersProfiles : Profile
    {
        public UsersProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Users, VolunteersUsersDto>();

            CreateMap<Users, EmployeesUsersDto>()
                 .ForMember(dest => dest.RoleName,
                                 opt => opt.MapFrom(src => src.Roles.RoleName));

            CreateMap<Users, UsersVehiclesDto>();


            CreateMap<UsersEmployeesForCreationDto, Users>();
            
            CreateMap<UsersVolunteersForCreationDto, Users>();

            CreateMap<UsersForUpdateDto, Users>();
            CreateMap<Users, UsersForUpdateDto>();


            /*Al momento de mapear, defino que el campo denominado token, sera devuelto con los valores obtenido de la funcion
              con el ID, y el RoleName de usuario logueado*/
            CreateMap<Users, UserAuthDto>()
                 .ForMember(dest => dest.token,
                                    opt => opt.MapFrom(src => UserSecurity.GenerateAccessToken(src.UserID, src.Roles.RoleName)))

                 .ForMember(dest => dest.RoleName,
                                    opt => opt.MapFrom(src => src.Roles.RoleName));
           
        }
    }
    
}



