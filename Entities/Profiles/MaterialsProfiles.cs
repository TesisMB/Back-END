using AutoMapper;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.ResourcesRequestMaterialsMedicinesVehicles___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class MaterialsProfiles : Profile
    {
        public MaterialsProfiles()
        {
            CreateMap<Materials, MaterialsDto>();

            CreateMap<Materials, ResourcesMaterialsDto>()


                .ForPath(dest => dest.Brand, opts => opts.MapFrom(src => src.MaterialBrand))

                .ForPath(dest => dest.Name, opts => opts.MapFrom(src => src.MaterialName));


            CreateMap<Resources_ForCreationDto, Materials>();

            CreateMap<Materials, Resources_Dto>()
                .ForMember(dest => dest.Name,
                            opt => opt.MapFrom(src => src.MaterialName))

                .ForPath(dest => dest.Materials.Brand, opts => opts.MapFrom(src => src.MaterialBrand))

                    .ForMember(dest => dest.Quantity,
                            opt => opt.MapFrom(src => src.MaterialQuantity))

                      .ForMember(dest => dest.Availability,
                            opt => opt.MapFrom(src => src.MaterialAvailability))

                        .ForMember(dest => dest.Picture,
                            opt => opt.MapFrom(src => src.MaterialPicture))

                        .ForMember(dest => dest.Description,
                                       opt => opt.MapFrom(src => src.MaterialUtility))

                           .ForMember(dest => dest.Description,
                                       opt => opt.MapFrom(src => src.MaterialUtility))

                           .ForPath(dest => dest.LocationCityName,
                                       opt => opt.MapFrom(src => src.Estates.Locations.LocationCityName));

            CreateMap<MaterialsForCreationDto, Materials>();

            CreateMap<MaterialsForUpdateDto, Materials>()
                .ForMember(a => a.MaterialQuantity, b => b.MapFrom(a => a.MaterialQuantity));

     

            CreateMap<Materials, MaterialsForUpdateDto>();

            CreateMap<Materials, Resources_ForCreationDto>()
                     .ForPath(a => a.Quantity, b => b.MapFrom(a => a.MaterialQuantity))
                     .ForPath(a => a.Name, b => b.MapFrom(a => a.MaterialName))
                     .ForPath(a => a.Availability, b => b.MapFrom(a => a.MaterialAvailability))
                     .ForPath(a => a.FK_EstateID, b => b.MapFrom(a => a.FK_EstateID))
                     .ForPath(a => a.Description, b => b.MapFrom(a => a.MaterialUtility))
                     .ForPath(a => a.Materials.Brand, b => b.MapFrom(a => a.MaterialBrand));
            


        }
    }
}
