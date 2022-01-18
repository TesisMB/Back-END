using AutoMapper;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class MaterialsProfiles : Profile
    {
        public MaterialsProfiles()
        {
            CreateMap<Materials, MaterialsDto>();

            CreateMap<Materials, Resources_Dto>()

                .ForMember(dest => dest.Name,
                            opt => opt.MapFrom(src => src.MaterialName))

                /*.ForMember(dest => dest.Materials.Mark,
                          opt => opt.MapFrom(src => src.MaterialMark))*/

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

            CreateMap<MaterialsForUpdateDto, Materials>();

            CreateMap<Brands, MaterialsForUpdateDto>();


        }
    }
}
