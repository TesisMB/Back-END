using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class EstateProfile : Profile
    {
        public EstateProfile()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Entities.Estate, Models.EstateDto>();
            
            CreateMap<Models.EstateForCreation_UpdateDto, Entities.Estate>();
            CreateMap<Entities.Estate,Models.EstateForCreation_UpdateDto> ();

        }
}
}

