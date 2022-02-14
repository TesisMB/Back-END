using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class ResourcesRequestMaterialsMedicinesVehiclesProfiles: Profile
    {
        public ResourcesRequestMaterialsMedicinesVehiclesProfiles()
        {
            CreateMap<ResourcesRequestMaterialsMedicinesVehicles, ResourcesRequestMaterialsMedicinesVehiclesDto>();


            CreateMap<ResourcesRequestMaterialsMedicinesVehiclesForCreationDto, ResourcesRequestMaterialsMedicinesVehicles>();

            CreateMap<ResourcesRequestMaterialsMedicinesVehicles, ResourcesRequestMaterialsMedicinesVehiclesForCreationDto>();

        }


    }
}
