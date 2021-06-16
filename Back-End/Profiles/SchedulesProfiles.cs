using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models.Persons___Dto;

namespace Back_End.Profiles
{
    public class SchedulesProfiles: Profile
    {
        public SchedulesProfiles()
        {
            CreateMap<Schedules, SchedulesDto>()
                                .ForMember(dest => dest.ScheduleID,
                                    opt => opt.MapFrom(src => src.ID));
        }
    }
}
