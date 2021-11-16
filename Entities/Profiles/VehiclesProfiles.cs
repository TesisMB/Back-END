
using AutoMapper;
using Back_End.Models;
using Back_End.Models.Vehicles___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.Vehicles___Dto.Creation;
using Entities.DataTransferObjects.Vehicles___Dto.Update;

namespace Entities.Profiles
{
    public class VehiclesProfiles : Profile
    {
        public VehiclesProfiles()
        {
            CreateMap<Vehicles, VehiclesDto>();

            CreateMap<Vehicles, Resources_Dto>()

               .ForMember(dest => dest.Name, opts => opts.MapFrom(src => $"{src.BrandsModels.Brands.BrandName } {src.BrandsModels.Model.ModelName}"))

                //.ForPath(dest => dest.Vehicles.Utility, opts => opts.MapFrom(src => src.VehicleUtility))

                .ForMember(dest => dest.Description,
                            opt => opt.MapFrom(src => src.VehicleDescription))

                  .ForMember(dest => dest.Availability,
                            opt => opt.MapFrom(src => src.VehicleAvailability))

                    .ForMember(dest => dest.Picture,
                            opt => opt.MapFrom(src => src.VehiclePicture))

                .ForPath(dest => dest.Vehicles.Type, opts => opts.MapFrom(src => src.Type.Type))

                 .ForPath(dest => dest.Vehicles.VehiclePatent, opts => opts.MapFrom(src => src.VehiclePatent))

                 .ForPath(dest => dest.Vehicles.VehicleYear, opts => opts.MapFrom(src => src.VehicleYear))

                 .ForPath(dest => dest.LocationCityName, opts => opts.MapFrom(src => src.Estates.Locations.LocationCityName));

            //.ForPath(dest => dest.Vehicles.Employees, opts => opts.MapFrom(src => src.Employees));

            //.ForPath(dest => dest.Vehicles.BrandsModels, opts => opts.MapFrom(src => src.BrandsModels));
            ;



            CreateMap<VehiclesForCreationDto, Vehicles>();

            CreateMap<VehiclesForUpdateDto, Vehicles>();
            CreateMap<Vehicles, VehiclesForUpdateDto>();
        }
    }
}
