using AutoMapper;
using Entities.DataTransferObjects.MarksModels___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
   public class MarksModelsProfiles : Profile
    {
        public MarksModelsProfiles()
        {
            CreateMap<MarksModels, MarksModelsDto>();
        }
    }
}
