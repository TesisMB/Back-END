using AutoMapper;
using Back_End.Entities;
using Back_End.Models.VolunteersSkills___Dto;
using Back_End.Models.VolunteersSkillsDto___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class VolunteersSkillsProfiles : Profile
    {
        public VolunteersSkillsProfiles()
        {
            CreateMap<VolunteersSkills, VolunteersSkillsDto>();
            CreateMap<VolunteersSkillsForCreationDto, VolunteersSkills>();

        }
    }
}
