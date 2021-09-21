using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
   public class Resources_VehiclesProfiles: Profile
    {
        public Resources_VehiclesProfiles()
        {
            CreateMap<Resources_Vehicles, Resources_VehiclesDto>();
            CreateMap<Resources_VehiclesForCreationDto, Resources_Vehicles>();
        }
    }
}
