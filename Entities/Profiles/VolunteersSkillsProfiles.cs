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
            CreateMap<VolunteersSkills, VolunteersSkillsDto>()
                .ForPath(a => a.SkillName, i => i.MapFrom(src => src.Skills.SkillName));

            CreateMap<VolunteersSkillsForCreationDto, VolunteersSkills>();

        }

    }
}
