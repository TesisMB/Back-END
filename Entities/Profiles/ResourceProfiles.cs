using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class ResourceProfiles: Profile
    {
        public ResourceProfiles()
        {
            CreateMap<Resources, ResourcesDto>();
            CreateMap<ResourcesForCreationDto, Resources>();
        }
    }
}
