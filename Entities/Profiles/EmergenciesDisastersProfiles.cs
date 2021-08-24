using AutoMapper;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class EmergenciesDisastersProfiles : Profile
    {
        public EmergenciesDisastersProfiles()
        {
            CreateMap<EmergenciesDisasters, EmergenciesDisastersDto>();

            CreateMap<EmergenciesDisastersForCreationDto, EmergenciesDisasters>();

            CreateMap<EmergenciesDisastersForUpdateDto, EmergenciesDisasters>();

            CreateMap<EmergenciesDisasters, EmergenciesDisastersForUpdateDto>();

        }
    }
}
