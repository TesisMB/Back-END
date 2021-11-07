using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Back_End.Models.Persons___Dto;

namespace Back_End.Profiles
{
    public class SchedulesProfiles : Profile
    {
        public SchedulesProfiles()
        {
            CreateMap<Schedules, SchedulesDto>();
        }
    }
}
