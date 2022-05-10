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

                               .ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.VehicleQuantity))


                               .ForPath(dest => dest.Name, opts => opts.MapFrom(src => src.Brands.BrandName + " " + src.Model.ModelName))

                               .ForPath(dest => dest.Type, opts => opts.MapFrom(src => src.TypeVehicles.Type));


            CreateMap<Vehicles, Resources_Dto>()

                .ForPath(dest => dest.Name, opts => opts.MapFrom(src => src.Brands.BrandName + " " + src.Model.ModelName))

                .ForMember(dest => dest.CreatedByEmployee,
                                opt => opt.MapFrom(src => src.EmployeeCreated.Users.Persons.FirstName + " " + src.EmployeeCreated.Users.Persons.LastName))


                .ForMember(dest => dest.ModifiedByEmployee,
                            opt => opt.MapFrom(src => src.EmployeeModified.Users.Persons.FirstName + " " + src.EmployeeModified.Users.Persons.LastName))

                .ForMember(dest => dest.Description,
                            opt => opt.MapFrom(src => src.VehicleDescription))
                
                .ForMember(dest => dest.Donation,
                            opt => opt.MapFrom(src => src.VehicleDonation))

                  .ForMember(dest => dest.Availability,
                            opt => opt.MapFrom(src => src.VehicleAvailability))

                 .ForMember(dest => dest.Quantity,
                                            opt => opt.MapFrom(src => src.VehicleQuantity))

                    .ForMember(dest => dest.Picture,
                            opt => opt.MapFrom(src => src.VehiclePicture))

                    .ForPath(dest => dest.Vehicles.VehicleUtility,
                            opt => opt.MapFrom(src => src.VehicleUtility))


                            .ForPath(dest => dest.Vehicles.FK_EmployeeID,
                            opt => opt.MapFrom(src => src.FK_EmployeeID))

                .ForPath(dest => dest.Vehicles.Type, opts => opts.MapFrom(src => src.TypeVehicles.Type))

                 .ForPath(dest => dest.Vehicles.VehiclePatent, opts => opts.MapFrom(src => src.VehiclePatent))

                 .ForPath(dest => dest.Vehicles.VehicleYear, opts => opts.MapFrom(src => src.VehicleYear))

                 .ForPath(dest => dest.LocationCityName, opts => opts.MapFrom(src => src.Estates.Locations.LocationCityName))
               
                 .ForPath(dest => dest.Vehicles.Fk_TypeVehicleID, opts => opts.MapFrom(src => src.Fk_TypeVehicleID))


               .ForPath(dest => dest.Vehicles.FK_BrandID, opts => opts.MapFrom(src => src.FK_BrandID))

               .ForPath(dest => dest.Vehicles.FK_ModelID, opts => opts.MapFrom(src => src.FK_ModelID))

               .ForPath(dest => dest.Vehicles.ModelName, opts => opts.MapFrom(src => src.Model.ModelName))
               .ForPath(dest => dest.Vehicles.BrandName, opts => opts.MapFrom(src => src.Brands.BrandName))

                 .ForPath(dest => dest.Vehicles.EmployeeName, opts => opts.MapFrom(src => $"{src.Employees.Users.Persons.FirstName} {src.Employees.Users.Persons.LastName}"));



            CreateMap<Resources_ForCreationDto, Vehicles>()
                 .ForPath(dest => dest.VehicleQuantity, opts => opts.MapFrom(src => 1))
                 .ForPath(dest => dest.VehicleDonation, opts => opts.MapFrom(src => src.Donation))
                 .ForPath(dest => dest.VehicleAvailability, opts => opts.MapFrom(src => src.Availability))
                 .ForPath(dest => dest.VehiclePicture, opts => opts.MapFrom(src => src.Picture))
                 .ForPath(dest => dest.VehicleUtility, opts => opts.MapFrom(src => src.Description))
                 .ForPath(a => a.VehicleDateCreated, b => b.MapFrom(a => a.DateCreated))

                 .ForPath(a => a.VehiclePatent, b => b.MapFrom(a => a.Vehicles.VehiclePatent))
                 .ForPath(a => a.VehicleYear, b => b.MapFrom(a => a.Vehicles.VehicleYear))
                 .ForPath(a => a.VehicleUtility, b => b.MapFrom(a => a.Vehicles.VehicleUtility))
                 .ForPath(a => a.FK_EmployeeID, b => b.MapFrom(a => a.Vehicles.FK_EmployeeID))


                 .ForPath(a => a.Fk_TypeVehicleID, b => b.MapFrom(a => a.Vehicles.Fk_TypeVehicleID))
                 .ForPath(a => a.Brands.BrandName, b => b.MapFrom(a => a.Vehicles.BrandName))
                 .ForPath(a => a.Model.ModelName, b => b.MapFrom(a => a.Vehicles.ModelName));

            CreateMap<Vehicles, VehiclesForCreationDto>();


            CreateMap<VehiclesForUpdateDto, Vehicles>()
                     .ForPath(a => a.VehicleQuantity, b => b.MapFrom(a => a.Quantity))
                     .ForPath(a => a.VehicleDonation, b => b.MapFrom(a => a.Donation))
                     .ForPath(a => a.VehicleAvailability, b => b.MapFrom(a => a.Availability))
                     .ForPath(a => a.VehiclePicture, b => b.MapFrom(a => a.Picture))
                     .ForPath(a => a.VehicleDescription, b => b.MapFrom(a => a.Description))
                     .ForPath(a => a.VehiclePatent, b => b.MapFrom(a => a.Vehicles.VehiclePatent))
                     .ForPath(a => a.VehicleYear, b => b.MapFrom(a => a.Vehicles.VehicleYear))
                     .ForPath(a => a.VehicleUtility, b => b.MapFrom(a => a.Vehicles.VehicleUtility))
                     .ForPath(a => a.FK_EmployeeID, b => b.MapFrom(a => a.Vehicles.FK_EmployeeID))
                     .ForPath(a => a.Fk_TypeVehicleID, b => b.MapFrom(a => a.Vehicles.Fk_TypeVehicleID));
                     //.ForPath(a => a.Brands.BrandName, b => b.MapFrom(a => a.Vehicles.BrandName))
                     //.ForPath(a => a.Model.ModelName, b => b.MapFrom(a => a.Vehicles.ModelName));


            CreateMap<Vehicles, VehiclesForUpdateDto>()
                     .ForPath(a => a.Quantity, b => b.MapFrom(a => a.VehicleQuantity))
                     .ForPath(a => a.Donation, b => b.MapFrom(a => a.VehicleDonation))
                     .ForPath(a => a.Availability, b => b.MapFrom(a => a.VehicleAvailability))
                     .ForPath(a => a.Picture, b => b.MapFrom(a => a.VehiclePicture))
                     .ForPath(a => a.Description, b => b.MapFrom(a => a.VehicleDescription))
                     .ForPath(a => a.Vehicles.VehiclePatent, b => b.MapFrom(a => a.VehiclePatent))
                     .ForPath(a => a.Vehicles.VehicleYear, b => b.MapFrom(a => a.VehicleYear))
                     .ForPath(a => a.Vehicles.VehicleUtility, b => b.MapFrom(a => a.VehicleUtility))
                     .ForPath(a => a.Vehicles.FK_EmployeeID, b => b.MapFrom(a => a.FK_EmployeeID))
                     .ForPath(a => a.Vehicles.Fk_TypeVehicleID, b => b.MapFrom(a => a.Fk_TypeVehicleID));
                     //.ForPath(a => a.Vehicles.BrandName, b => b.MapFrom(a => a.Brands.BrandName))
                     //.ForPath(a => a.Vehicles.ModelName, b => b.MapFrom(a => a.Model.ModelName)); 

        }
    }
}
