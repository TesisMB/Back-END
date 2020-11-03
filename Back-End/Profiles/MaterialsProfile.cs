using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class MaterialsProfile : Profile
    {
        public MaterialsProfile()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Entities.Materials, Models.MaterialsDto>();

            CreateMap<Models.MaterialsForCreation_UpdateDto, Entities.Materials>();
            CreateMap<Entities.Materials, Models.MaterialsForCreation_UpdateDto>();

        }
    }
}
