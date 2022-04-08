using AutoMapper;
using Entities.DataTransferObjects.BrandsModels__Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class BrandsModelsProfiles: Profile
    {
        public BrandsModelsProfiles()
        {
            CreateMap<BrandsModels, BrandsModelsForSelectDto>()

             .ForPath(resp => resp.BrandID, opt => opt.MapFrom(a => a.FK_BrandID))

             .ForPath(resp => resp.BrandsName, opt => opt.MapFrom(a => a.Brands.BrandName))

             .ForPath(resp => resp.ModelID, opt => opt.MapFrom(a => a.Model.ID))
             .ForPath(resp => resp.ModelName, opt => opt.MapFrom(a => a.Model.ModelName))

             .ForPath(resp => resp.TypeID, opt => opt.MapFrom(a => a.Vehicles.TypeVehicles.ID))
             .ForPath(resp => resp.Type, opt => opt.MapFrom(a => a.Vehicles.TypeVehicles.Type));

            }

    }
}
