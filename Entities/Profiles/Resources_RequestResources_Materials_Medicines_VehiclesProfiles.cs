using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class Resources_RequestResources_Materials_Medicines_VehiclesProfiles: Profile
    {
        public Resources_RequestResources_Materials_Medicines_VehiclesProfiles()
        {
            CreateMap<Resources_RequestResources_Materials_Medicines_Vehicles, Resources_RequestResources_Materials_Medicines_VehiclesDto>();

            CreateMap<Resources_RequestResources_Materials_Medicines_Vehicles, ResourcesDto>();


            CreateMap<Resources_RequestResources_Materials_Medicines_VehiclesForCreationDto, Resources_RequestResources_Materials_Medicines_Vehicles>();

            CreateMap<Resources_RequestResources_Materials_Medicines_Vehicles, Resources_RequestResources_Materials_Medicines_VehiclesForCreationDto>();

        }


    }
}
