using AutoMapper;
using Back_End.Models;
using Back_End.Models.VolunteersSkills___Dto;
using Back_End.Models.VolunteersSkillsDto___Dto;

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
