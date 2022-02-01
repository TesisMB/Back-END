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

            CreateMap<Resources_RequestResources_Materials_Medicines_VehiclesForCreationDto, Resources_RequestResources_Materials_Medicines_Vehicles>()
                
                .ForPath(a =>a.Resources_Materials.FK_MaterialID, i => i.MapFrom(src => src.FK_MaterialID))
                .ForPath(a =>a.Resources_Medicines.FK_MedicineID, i => i.MapFrom(src => src.FK_MedicineID))

                .ForPath(a =>a.Resources_Materials.Quantity, i => i.MapFrom(src => src.Quantity))
                .ForPath(a =>a.Resources_Medicines.Quantity, i => i.MapFrom(src => src.Quantity));
        }
    }
}
