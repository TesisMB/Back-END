using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Back_End.Models.Users___Dto.Users___Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class PersonsProfiles:Profile
    {
        public PersonsProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Persons, PersonsDto>()

                .ForMember(dest => dest.PersonID,
                                    opt => opt.MapFrom(src => src.ID));

            CreateMap<Persons, Users_PersonsDto>()

              .ForMember(dest => dest.PersonID,
                                    opt => opt.MapFrom(src => src.ID));
         

            CreateMap<PersonForCreationDto, Persons>();

            CreateMap<PersonsForUpdatoDto, Persons>();
            CreateMap<Persons, PersonsForUpdatoDto>();


            CreateMap<UserAuthDto, Persons>();

        }
    }
}
