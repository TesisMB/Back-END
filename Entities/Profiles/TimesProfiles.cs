using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.ResourcesDto;

namespace Back_End.Profiles
{
    public class TimesProfiles : Profile
    {
        public TimesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Times, TimesDto>();
                //.ForPath(opt => opt.ScheduleDate, src => src.MapFrom(a => a.Schedules.ScheduleDate)); ;
        }
    }
}
