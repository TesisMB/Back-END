using AutoMapper;
using Back_End.Dto;
using Back_End.Models;

namespace SICREYD.Profiles
{
    public class UsersProfile : Profile
    {

        public UsersProfile()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Users, UsersDto>()


                    //Creo dos variables nuevas no existentes en la Base de datos

                    //Mapeo entre Name y LastName de Users para devolver FullName (uniendo ambos valores)
                    .ForMember(
                        dest => dest.UserFullName,
                        opt => opt.MapFrom(src => $"{src.UserFirstName} {src.UserLastName}"))


                       .ForMember(dest => dest.RoleName,
                                    opt => opt.MapFrom(src => src.Roles.RoleName));




            CreateMap<UsersForCreationDto, Users>();
            CreateMap<UsersForUpdate, Users>();
            CreateMap<Users, UsersForUpdate>();


        }


    };
}


