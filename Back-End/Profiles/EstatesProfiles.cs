using AutoMapper;
using Back_End.Entities;
using Back_End.Models.Employees___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class EstatesProfiles : Profile
    {
        public EstatesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Estates, EstatesDto>()

                .ForMember(dest => dest.EstateID,
                                    opt => opt.MapFrom(src => src.ID));

        }
    }
}
