using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;

namespace Back_End.Profiles
{
    public class SkillsProfiles: Profile
    {
        public SkillsProfiles()
        {
            CreateMap<Skills, SkillsDto>();
            CreateMap<SkillsForCreationDto, Skills>();
        }
    }
}
