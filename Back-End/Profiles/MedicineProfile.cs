using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Entities.Medicine, Models.MedicineDto>();

            CreateMap<Models.MedicineForCreation_UpdateDto, Entities.Medicine>();
            CreateMap<Entities.Medicine, Models.MedicineForCreation_UpdateDto>();

        }
    }
}
