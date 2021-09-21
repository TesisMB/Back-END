using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
   public class Resources_MaterialsProfiles : Profile
    {
        public Resources_MaterialsProfiles()
        {
            CreateMap<Resources_Materials, Resources_MaterialsDto>();
            CreateMap<Resources_MaterialsForCreationDto, Resources_Materials>();
        }
    }
}
