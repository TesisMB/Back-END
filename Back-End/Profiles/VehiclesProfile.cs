using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class VehiclesProfile : Profile
    {
        public VehiclesProfile()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Entities.Vehicles, Models.VehiclesDto>();

            CreateMap<Models.VehiclesForCreation_UpdateDto, Entities.Vehicles>();
            CreateMap<Entities.Vehicles, Models.VehiclesForCreation_UpdateDto>();

        }
    }
}
