using AutoMapper;
using Entities.DataTransferObjects.Marks___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class MarksProfiles : Profile
    {
        public MarksProfiles()
        {
            CreateMap<Brands, BrandsDto>();
        }
    }
}
