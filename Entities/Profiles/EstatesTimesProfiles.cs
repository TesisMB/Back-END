using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Entities.Helpers;

namespace Back_End.Profiles
{
    public class EstatesTimesProfiles : Profile
    {
        public EstatesTimesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<EstatesTimes, EstatesTimesDto>()
                .ForPath(src => src.Times, opt => opt.MapFrom(a => $"{DateTimeOffsetExtensions.GetTime(a.Times.StartTime)} - {DateTimeOffsetExtensions.GetTime(a.Times.EndTime)}"))


                .ForPath(src => src.ScheduleDate, opt => opt.MapFrom(a => a.Times.Schedules.ScheduleDate));
        }
    }
}
