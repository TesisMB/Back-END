using AutoMapper;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class MaterialsProfiles : Profile
    {
        public MaterialsProfiles()
        {
            CreateMap<Materials, MaterialsDto>();

            CreateMap<MaterialsForCreationDto, Materials>();

            CreateMap<MaterialsForUpdateDto, Materials>();

            CreateMap<Materials, MaterialsForUpdateDto>();


        }
    }
}
