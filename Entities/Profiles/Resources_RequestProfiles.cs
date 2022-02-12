using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.Helpers;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

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


            CreateMap<Resource_RequestForUpdateDto, Resources_Request>()
                          //.ForMember(d => d.Resources_RequestResources_Materials_Medicines_Vehicles, opt => opt.Ignore());
                          .ForMember(a => a.Resources_RequestResources_Materials_Medicines_Vehicles, b => b.MapFrom(c => c.Resources_RequestResources_Materials_Medicines_Vehicles));


        }
    }
}