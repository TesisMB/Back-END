using AutoMapper;
using Back_End.Models;
using Back_End.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.ResourcesRequestMaterialsMedicinesVehicles___Dto;
using Entities.DataTransferObjects.Vehicles___Dto;
using Entities.DataTransferObjects.Vehicles___Dto.Creation;
using Entities.DataTransferObjects.Vehicles___Dto.Update;

namespace Entities.Profiles
{
    public class VehiclesProfiles : Profile
    {
        public VehiclesProfiles()
        {
            CreateMap<Vehicles, VehiclesDto>()

               .ForPath(dest => dest.Type, opts => opts.MapFrom(src => src.TypeVehicles.Type));

            CreateMap<Vehicles, ResourcesVehiclesDto>()

                               .ForPath(dest => dest.Name, opts => opts.MapFrom(src => src.BrandsModels.Brands.BrandName + " " + src.BrandsModels.Model.ModelName))

                               .ForPath(dest => dest.Type, opts => opts.MapFrom(src => src.TypeVehicles.Type));


            CreateMap<Vehicles, Resources_Dto>()

               .ForMember(dest => dest.Name, opts => opts.MapFrom(src => $"{src.BrandsModels.Brands.BrandName } {src.BrandsModels.Model.ModelName}"))

                .ForMember(dest => dest.Description,
                            opt => opt.MapFrom(src => src.VehicleDescription))

                  .ForMember(dest => dest.Availability,
                            opt => opt.MapFrom(src => src.VehicleAvailability))

                    .ForMember(dest => dest.Picture,
                            opt => opt.MapFrom(src => src.VehiclePicture))

                .ForPath(dest => dest.Vehicles.Type, opts => opts.MapFrom(src => src.TypeVehicles.Type))

                 .ForPath(dest => dest.Vehicles.VehiclePatent, opts => opts.MapFrom(src => src.VehiclePatent))

                 .ForPath(dest => dest.Vehicles.VehicleYear, opts => opts.MapFrom(src => src.VehicleYear))

                 .ForPath(dest => dest.LocationCityName, opts => opts.MapFrom(src => src.Estates.Locations.LocationCityName))

                 .ForPath(dest => dest.Vehicles.EmployeeName, opts => opts.MapFrom(src => $"{src.Employees.Users.Persons.FirstName} {src.Employees.Users.Persons.LastName}"));
            ;



            CreateMap<VehiclesForCreationDto, Vehicles>();

            CreateMap<VehiclesForUpdateDto, Vehicles>();
            CreateMap<Vehicles, VehiclesForUpdateDto>();
        }
    }
}
