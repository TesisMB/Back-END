using AutoMapper;
using Entities.DataTransferObjects.Alerts___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
   public class AlertsProfiles : Profile
    {
        public AlertsProfiles()
        {
            CreateMap<Alerts, AlertsDto>();
        }
    }
}
