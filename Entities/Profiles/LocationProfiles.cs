using AutoMapper;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class LocationProfiles : Profile
    {
        public LocationProfiles()
        {
            CreateMap<Locations, LocationsDto>();
        }
    }
}
