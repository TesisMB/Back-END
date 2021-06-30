using AutoMapper;
using Back_End.Models;
using Back_End.Models.TypeVehicles___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class TypeVehiclesProfiles : Profile
    {
        public TypeVehiclesProfiles()
        {
            CreateMap<TypeVehicles, TypeVehiclesDto>();
        }
    }
}
