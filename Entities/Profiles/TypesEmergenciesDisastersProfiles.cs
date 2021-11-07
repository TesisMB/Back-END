using AutoMapper;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class TypesEmergenciesDisastersProfiles : Profile
    {
        public TypesEmergenciesDisastersProfiles()
        {
            CreateMap<TypesEmergenciesDisasters, TypesEmergenciesDisastersDto>();
        }
    }
}
