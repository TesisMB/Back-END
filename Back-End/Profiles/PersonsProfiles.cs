using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
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

                .ForMember(dest => dest.Status,
                                    opt => opt.MapFrom(src => src.Available));
         
            CreateMap<PersonForCreationDto, Persons>();

            CreateMap<PersonsForUpdatoDto, Persons>();

            CreateMap<UserAuthDto, Persons>();

        }
    }
}
