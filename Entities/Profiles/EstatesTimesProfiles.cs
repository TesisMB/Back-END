using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;

namespace Back_End.Profiles
{
    public class EstatesTimesProfiles : Profile
    {
        public EstatesTimesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<EstatesTimes, EstatesTimesDto>()
                .ForPath(src => src.Times, opt => opt.MapFrom(a => $"{a.Times.StartTime} - {a.Times.EndTime}"))
               // .ForPath(src => src.StartTime, opt => opt.MapFrom(a => a.Times.StartTime))

                //.ForPath(src => src.EndTime, opt => opt.MapFrom(a => a.Times.EndTime))

                .ForPath(src => src.ScheduleDate, opt => opt.MapFrom(a => a.Times.Schedules.ScheduleDate));
        }
    }
}
