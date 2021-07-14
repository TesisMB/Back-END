using AutoMapper;
using Entities.DataTransferObjects.Models___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class ModelsProfiles : Profile
    {
        public ModelsProfiles()
        {
            CreateMap<Model, ModelsDto>();
        }
    }
}
