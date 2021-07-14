using AutoMapper;
using Entities.DataTransferObjects.Marks___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class MarksProfiles : Profile
    {
        public MarksProfiles()
        {
            CreateMap<Marks, MarksDto>();
        }
    }
}
