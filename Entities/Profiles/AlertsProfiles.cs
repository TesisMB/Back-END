using AutoMapper;
using Entities.DataTransferObjects.Alerts___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class AlertsProfiles : Profile
    {
        public AlertsProfiles()
        {
            CreateMap<Alerts, AlertsDto>();

            CreateMap<AlertsDto, Alerts>();
        }
    }
}
