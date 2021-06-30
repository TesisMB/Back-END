using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class RolesProfiles : Profile
    {
        public RolesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Roles, RolesDto>();
            CreateMap<RolesForCreationDto, Roles>();

        }
    }
}
