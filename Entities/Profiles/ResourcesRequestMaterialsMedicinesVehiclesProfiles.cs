using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.Helpers;
using Entities.Models;

namespace Entities.Profiles
{
    public class ResourcesRequestMaterialsMedicinesVehiclesProfiles: Profile
    {
        public ResourcesRequestMaterialsMedicinesVehiclesProfiles()
        {
            CreateMap<ResourcesRequestMaterialsMedicinesVehicles, ResourcesRequestMaterialsMedicinesVehiclesDto>()

                 .ForPath(dest => dest.MedicineExpirationDate, opts => opts.MapFrom(src => DateTimeOffsetExtensions.GetDate(src.Medicines.MedicineExpirationDate)))


                         .ForPath(dest => dest.Brand, opts => opts.MapFrom(src => src.Materials.MaterialBrand))

                                .ForPath(dest => dest.Type, opts => opts.MapFrom(src => src.Vehicles.TypeVehicles.Type))


                    .ForPath(dest => dest.VehicleYear, opts => opts.MapFrom(src => src.Vehicles.VehicleYear))

                    .ForPath(dest => dest.VehiclePatent, opts => opts.MapFrom(src => src.Vehicles.VehiclePatent))

                          .ForMember(dest => dest.MedicineLab,
                            opt => opt.MapFrom(src => src.Medicines.MedicineLab))


                          .ForMember(dest => dest.MedicineDrug,
                            opt => opt.MapFrom(src => src.Medicines.MedicineDrug))


                          .ForMember(dest => dest.MedicineWeight,
                            opt => opt.MapFrom(src => src.Medicines.MedicineWeight))


                           .ForMember(dest => dest.MedicineUnits,
                            opt => opt.MapFrom(src => src.Medicines.MedicineUnits)); ;



            CreateMap<ResourcesRequestMaterialsMedicinesVehiclesForCreationDto, ResourcesRequestMaterialsMedicinesVehicles>();

            CreateMap<ResourcesRequestMaterialsMedicinesVehicles, ResourcesRequestMaterialsMedicinesVehiclesForCreationDto>();

        }


    }
}
