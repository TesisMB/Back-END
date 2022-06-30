using AutoMapper;
using Entities.DataTransferObjects.Materials___Dto;
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

            CreateMap<Materials, Resources_Dto>();
                //.ForMember(dest => dest.Name,
                //            opt => opt.MapFrom(src => src.MaterialName))

                //.ForMember(dest => dest.Donation,
                //            opt => opt.MapFrom(src => src.MaterialDonation))

                //.ForPath(dest => dest.Materials.Brand, opts => opts.MapFrom(src => src.MaterialBrand))

                //    .ForMember(dest => dest.Quantity,
                //            opt => opt.MapFrom(src => src.MaterialQuantity))

                //      .ForMember(dest => dest.Availability,
                //            opt => opt.MapFrom(src => src.MaterialAvailability))

                //        .ForMember(dest => dest.Picture,
                //            opt => opt.MapFrom(src => src.MaterialPicture))

                //        .ForMember(dest => dest.Description,
                //                       opt => opt.MapFrom(src => src.MaterialUtility))

                //           .ForMember(dest => dest.Description,
                //                       opt => opt.MapFrom(src => src.MaterialUtility))

                //           .ForMember(dest => dest.CreatedByEmployee,
                //                       opt => opt.MapFrom(src => src.EmployeeCreated.Users.Persons.FirstName + " " + src.EmployeeCreated.Users.Persons.LastName))

                //           .ForMember(dest => dest.ModifiedByEmployee,
                //                       opt => opt.MapFrom(src => src.EmployeeModified.Users.Persons.FirstName + " " + src.EmployeeModified.Users.Persons.LastName))

                //           .ForPath(dest => dest.LocationCityName,
                //                       opt => opt.MapFrom(src => src.Estates.Locations.LocationCityName));

            CreateMap<Materials, Resources_ForCreationDto>();



            CreateMap<Resources_ForCreationDto, Materials>()
                     .ForPath(a => a.MaterialQuantity, b => b.MapFrom(a => a.Quantity))
                     .ForPath(a => a.MaterialDonation, b => b.MapFrom(a => a.Donation))
                     .ForPath(a => a.MaterialName, b => b.MapFrom(a => a.Name))
                     .ForPath(a => a.MaterialAvailability, b => b.MapFrom(a => a.Availability))
                     .ForPath(a => a.MaterialPicture, b => b.MapFrom(a => a.Picture))
                     .ForPath(a => a.MaterialUtility, b => b.MapFrom(a => a.Description))
                     .ForPath(a => a.MaterialDateCreated, b => b.MapFrom(a => a.DateCreated))
                     .ForPath(a => a.MaterialBrand, b => b.MapFrom(a => a.Materials.Brand));




            CreateMap<Materials, MaterialsForUpdateDto>()
                     .ForPath(a => a.Quantity, b => b.MapFrom(a => a.MaterialQuantity))
                     .ForPath(a => a.Donation, b => b.MapFrom(a => a.MaterialDonation))
                     .ForPath(a => a.Name, b => b.MapFrom(a => a.MaterialName))
                     .ForPath(a => a.Availability, b => b.MapFrom(a => a.MaterialAvailability))
                     .ForPath(a => a.Picture, b => b.MapFrom(a => a.MaterialPicture))
                     .ForPath(a => a.Description, b => b.MapFrom(a => a.MaterialUtility))
                     .ForPath(a => a.DateModified, b => b.MapFrom(a => a.MaterialDateModified))
                     .ForPath(a => a.Materials.Brand, b => b.MapFrom(a => a.MaterialBrand));

            CreateMap<MaterialsForUpdateDto, Materials>()

                     .ForPath(a => a.MaterialQuantity, b => b.MapFrom(a => a.Quantity))
                     .ForPath(a => a.MaterialDonation, b => b.MapFrom(a => a.Donation))
                     .ForPath(a => a.MaterialName, b => b.MapFrom(a => a.Name))
                     .ForPath(a => a.MaterialAvailability, b => b.MapFrom(a => a.Availability))
                     .ForPath(a => a.MaterialDateModified, b => b.MapFrom(a => a.DateModified))
                     .ForPath(a => a.MaterialPicture, b => b.MapFrom(a => a.Picture))
                     .ForPath(a => a.MaterialUtility, b => b.MapFrom(a => a.Description))
                     .ForPath(a => a.MaterialBrand, b => b.MapFrom(a => a.Materials.Brand));

        }
    }
}
