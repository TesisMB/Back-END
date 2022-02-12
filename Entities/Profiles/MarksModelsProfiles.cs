using AutoMapper;
using Entities.DataTransferObjects.MarksModels___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class MarksModelsProfiles : Profile
    {
        public MarksModelsProfiles()
        {
            CreateMap<BrandsModels, BrandsModelsDto>();
        }
    }
}
