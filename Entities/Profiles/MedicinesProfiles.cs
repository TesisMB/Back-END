using AutoMapper;
using Entities.DataTransferObjects.Medicines___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.ResourcesRequestMaterialsMedicinesVehicles___Dto;
using Entities.Helpers;
using Entities.Models;

namespace Entities.Profiles
{
    public class MedicinesProfiles : Profile
    {

        public MedicinesProfiles()
        {
            CreateMap<Medicines, MedicinesDto>()
                                     .ForPath(dest => dest.MedicineExpirationDate, opts => opts.MapFrom(src => DateTimeOffsetExtensions.GetDateToMedicine(src.MedicineExpirationDate)));



            CreateMap<Medicines, ResourcesMedicnesDto>()
                                .ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.MedicineQuantity))

                .ForPath(dest => dest.MedicineExpirationDate, opts => opts.MapFrom(src => DateTimeOffsetExtensions.GetDateToMedicine(src.MedicineExpirationDate)))




                .ForMember(dest => dest.Name,
                            opt => opt.MapFrom(src => src.MedicineName));


            CreateMap<Medicines, Resources_Dto>()

                    .ForMember(dest => dest.Name,
                                opt => opt.MapFrom(src => src.MedicineName))

                   .ForMember(dest => dest.CreatedByEmployee,
                                    opt => opt.MapFrom(src => src.EmployeeCreated.Users.Persons.FirstName + " " + src.EmployeeCreated.Users.Persons.LastName))


                    .ForMember(dest => dest.ModifiedByEmployee,
                                opt => opt.MapFrom(src => src.EmployeeModified.Users.Persons.FirstName + " " + src.EmployeeModified.Users.Persons.LastName))

                    .ForMember(dest => dest.Donation,
                                opt => opt.MapFrom(src => src.MedicineDonation))

                     .ForMember(dest => dest.Quantity,
                                opt => opt.MapFrom(src => src.MedicineQuantity))

                     .ForMember(dest => dest.Description,
                                opt => opt.MapFrom(src => src.MedicineUtility))

                     .ForMember(dest => dest.Availability,
                                opt => opt.MapFrom(src => src.MedicineAvailability))

                     .ForPath(dest => dest.Medicines.MedicineExpirationDate, opts => opts.MapFrom(src => DateTimeOffsetExtensions.GetDateToMedicine(src.MedicineExpirationDate)))


                     .ForPath(dest => dest.Medicines.MedicineLab, opts => opts.MapFrom(src => src.MedicineLab))


                     .ForPath(dest => dest.Medicines.MedicineDrug, opts => opts.MapFrom(src => src.MedicineDrug))

                     .ForPath(dest => dest.Medicines.MedicineWeight, opts => opts.MapFrom(src => src.MedicineWeight))


                     .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.MedicinePicture))

                     .ForPath(dest => dest.Medicines.MedicineUnits, opts => opts.MapFrom(src => src.MedicineUnits))

                     .ForPath(dest => dest.LocationCityName, opts => opts.MapFrom(src => src.Estates.Locations.LocationCityName));



            CreateMap<Resources_ForCreationDto, Medicines>()
                  .ForMember(dest => dest.MedicineQuantity, opts => opts.MapFrom(src => src.Quantity))
                 .ForMember(dest => dest.MedicineDonation, opts => opts.MapFrom(src => src.Donation))
                 .ForMember(dest => dest.MedicineName, opts => opts.MapFrom(src => src.Name))
                 .ForMember(dest => dest.MedicineAvailability, opts => opts.MapFrom(src => src.Availability))
                 .ForMember(dest => dest.MedicinePicture, opts => opts.MapFrom(src => src.Picture))
                 .ForMember(dest => dest.MedicineUtility, opts => opts.MapFrom(src => src.Description))
                 .ForPath(a => a.MedicineDateCreated, b => b.MapFrom(a => a.DateCreated))

                 .ForPath(dest => dest.MedicineExpirationDate, opts => opts.MapFrom(src => src.Medicines.MedicineExpirationDate))
                 .ForPath(dest => dest.MedicineLab, opts => opts.MapFrom(src => src.Medicines.MedicineLab))
                 .ForPath(dest => dest.MedicineDrug, opts => opts.MapFrom(src => src.Medicines.MedicineDrug))
                 .ForPath(dest => dest.MedicineWeight, opts => opts.MapFrom(src => src.Medicines.MedicineWeight))
                 .ForPath(dest => dest.MedicineUnits, opts => opts.MapFrom(src => src.Medicines.MedicineUnits)); ;


            CreateMap<MedicineForUpdateDto, Medicines>()
                     .ForPath(a => a.MedicineQuantity, b => b.MapFrom(a => a.Quantity))
                     .ForPath(a => a.MedicineDonation, b => b.MapFrom(a => a.Donation))
                     .ForPath(a => a.MedicineName, b => b.MapFrom(a => a.Name))
                     .ForPath(a => a.MedicineAvailability, b => b.MapFrom(a => a.Availability))
                     .ForPath(a => a.MedicinePicture, b => b.MapFrom(a => a.Picture))
                     .ForPath(a => a.MedicineUtility, b => b.MapFrom(a => a.Description))
                     .ForPath(a => a.MedicineExpirationDate, b => b.MapFrom(a => a.Medicines.MedicineExpirationDate))
                     .ForPath(a => a.MedicineLab, b => b.MapFrom(a => a.Medicines.MedicineLab))
                     .ForPath(a => a.MedicineDateModified, b => b.MapFrom(a => a.DateModified))
                     .ForPath(a => a.MedicineDrug, b => b.MapFrom(a => a.Medicines.MedicineDrug))
                     .ForPath(a => a.MedicineWeight, b => b.MapFrom(a => a.Medicines.MedicineWeight))
                     .ForPath(a => a.MedicineUnits, b => b.MapFrom(a => a.Medicines.MedicineUnits));


            CreateMap<Medicines, MedicineForUpdateDto>()
                    .ForPath(a => a.Quantity, b => b.MapFrom(a => a.MedicineQuantity))
                     .ForPath(a => a.Donation, b => b.MapFrom(a => a.MedicineDonation))
                     .ForPath(a => a.Name, b => b.MapFrom(a => a.MedicineName))
                     .ForPath(a => a.Availability, b => b.MapFrom(a => a.MedicineAvailability))
                     .ForPath(a => a.Picture, b => b.MapFrom(a => a.MedicinePicture))
                     .ForPath(a => a.Description, b => b.MapFrom(a => a.MedicineUtility))
                     .ForPath(a => a.DateModified, b => b.MapFrom(a => a.MedicineDateModified))
                     .ForPath(a => a.Medicines.MedicineExpirationDate, b => b.MapFrom(a => a.MedicineExpirationDate))
                     .ForPath(a => a.Medicines.MedicineLab, b => b.MapFrom(a => a.MedicineLab))
                     .ForPath(a => a.Medicines.MedicineDrug, b => b.MapFrom(a => a.MedicineDrug))
                     .ForPath(a => a.Medicines.MedicineWeight, b => b.MapFrom(a => a.MedicineWeight))
                     .ForPath(a => a.Medicines.MedicineUnits, b => b.MapFrom(a => a.MedicineUnits));
        }
    }
}
