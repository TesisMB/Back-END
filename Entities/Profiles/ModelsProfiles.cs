using AutoMapper;
using Entities.DataTransferObjects.Models___Dto;
using Entities.Models;

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
