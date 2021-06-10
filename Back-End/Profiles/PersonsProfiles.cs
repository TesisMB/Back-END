using AutoMapper;
using Back_End.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Entities;
using Back_End.Models;

namespace Back_End.Profiles
{
    public class PersonsProfile : Profile
    {
        public PersonsProfile()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Entities.Persons, Models.PersonsDto>();

                     

            //Mapeo la Fecha de Nacimiento para devolver solamente la edad
            /*.ForMember(
                dest => dest.Age,
                opt => opt.MapFrom(src => src.UserBirthdate.GetCurrentAge())); */

            CreateMap<Models.PersonForCreationDto, Entities.Persons>();

                       //Creo dos variables nuevas no existentes en la Base de datos                         
                   /*    .ForMember(dest => dest.Users.UserDni,
                                    opt => opt.MapFrom(src => src.UserDni))

            CreateMap<PersonsForUpdatoDto, Persons>();
            CreateMap<Persons, PersonsForUpdatoDto>();


                      /*  .ForMember(dest => dest.UserAvailability,
                                    opt => opt.MapFrom(src => src.Users.UserAvailability))

                         .ForMember(dest => dest.RoleID,
                                    opt => opt.MapFrom(src => src.Users.FK_RoleID))


                         .ForMember(dest => dest.RoleName,
                                    opt => opt.MapFrom(src => src.Users.Roles.RoleName));*/
           // CreateMap<Models.UsersForUpdate, Entities.Users>();
           // CreateMap<Entities.Users, Models.UsersForUpdate>();

        }
    }
}



