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
            CreateMap<Resources_Materials, Resources_MaterialsDto>()
                                .ForPath(dest => dest.Materials.Brand, opts => opts.MapFrom(src => src.Materials.MaterialBrand));
            
            CreateMap<Resources_MaterialsForCreationDto, Resources_Materials>();
        }
    }
}
