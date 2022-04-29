using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.DataTransferObjects.ResourcesRequest___Dto;
using Entities.Helpers;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Profiles
{
    public class ResourcesRequestProfiles : Profile
    {

        public ResourcesRequestProfiles()
        {
            CreateMap<ResourcesRequest, ResourcesRequestDto>()

                 .ForPath(dest => dest.RequestDate, opts => opts.MapFrom(src => DateTimeOffsetExtensions.GetDate(src.RequestDate)));





            CreateMap<ResourcesRequestForCreationDto, ResourcesRequest>();

            CreateMap<AcceptRejectRequestDto, ResourcesRequest>();

            CreateMap<ResourcesRequest, ResourceRequestForUpdateDto>();


            CreateMap<ResourceRequestForUpdateDto, ResourcesRequest>()
                          //.ForMember(d => d.Resources_RequestResources_Materials_Medicines_Vehicles, opt => opt.Ignore());
                          .ForMember(a => a.Resources_RequestResources_Materials_Medicines_Vehicles, b => b.MapFrom(c => c.Resources_RequestResources_Materials_Medicines_Vehicles));


        }
    }
}