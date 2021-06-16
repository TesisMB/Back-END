using AutoMapper;
using Back_End.Entities;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Volunteers__Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class VolunteersProfiles : Profile
    {
        public VolunteersProfiles()
        {
            CreateMap<Volunteers, VolunteersDto>();

            CreateMap<VolunteersForCreationDto, Volunteers>();
            CreateMap<VolunteersForUpdatoDto, Volunteers>();
            CreateMap<Volunteers, VolunteersForUpdatoDto>();
        }
    }
}
