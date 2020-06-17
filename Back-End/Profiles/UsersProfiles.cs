using AutoMapper;
using Back_End.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class UsersProfile : Profile
    {

        public UsersProfile()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Entities.Users, Models.UsersDto>()


                    //Creo dos variables nuevas no existentes en la Base de datos

                    //Mapeo entre Name y LastName de Users para devolver FullName (uniendo ambos valores)
                    .ForMember(
                        dest => dest.FullName,
                        opt => opt.MapFrom(src => $"{src.UserFirstName} {src.UserLastname}"))

                       .ForMember(dest => dest.RoleName,
                                    opt => opt.MapFrom(src => src.RoleName))


                    //Mapeo la Fecha de Nacimiento para devolver solamente la edad
                    .ForMember(
                        dest => dest.Age,
                        opt => opt.MapFrom(src => src.UserBirthdate.GetCurrentAge()));
                        

            CreateMap<Models.UsersForCreationDto, Entities.Users>();
            CreateMap<Models.UsersForUpdate, Entities.Users>();
            CreateMap<Entities.Users, Models.UsersForUpdate>();


        }


    };
}


