using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.Helpers;
using Entities.Models;

namespace Entities.Profiles
{
    public class Resources_RequestProfiles : Profile
    {
        public Resources_RequestProfiles()
        {
            CreateMap<Resources_Request, Resources_RequestDto>()
                   .ForPath(dest => dest.RequestDate, opts => opts.MapFrom(src => DateTimeOffsetExtensions.GetDate(src.RequestDate)));
            
            CreateMap<Resources_Request, ResourcesDto>();

            CreateMap<Resources_RequestForCreationDto, Resources_Request>();

            CreateMap<Resources_Request, Resource_RequestForUpdateDto>();
            CreateMap<Resource_RequestForUpdateDto, Resources_Request>();

        }
    }
}
