using AutoMapper;
using Entities.DataTransferObjects.Medicines___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
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
                .ForPath(dest => dest.MedicineExpirationDate, opts => opts.MapFrom(src => DateTimeOffsetExtensions.GetDateToMedicine(src.MedicineExpirationDate)))




                .ForMember(dest => dest.Name,
                            opt => opt.MapFrom(src => src.MedicineName));


            CreateMap<Medicines, Resources_Dto>()

                .ForMember(dest => dest.Name,
                            opt => opt.MapFrom(src => src.MedicineName))
                
                .ForMember(dest => dest.Donation,
                            opt => opt.MapFrom(src => src.MedicineDonation))

                  .ForMember(dest => dest.Quantity,
                            opt => opt.MapFrom(src => src.MedicineQuantity))

                    .ForMember(dest => dest.Description,
                            opt => opt.MapFrom(src => src.MedicineUtility))

                      .ForMember(dest => dest.Availability,
                            opt => opt.MapFrom(src => src.MedicineAvailability))

                     .ForPath(dest => dest.Medicines.MedicineExpirationDate, opts => opts.MapFrom(src => DateTimeOffsetExtensions.GetDateToMedicine(src.MedicineExpirationDate)))

                     /* .ForMember(dest => dest.Medicines.MedicineExpirationDate,
                            opt => opt.MapFrom(src => src.MedicineExpirationDate))*/

                     .ForPath(dest => dest.Medicines.MedicineLab, opts => opts.MapFrom(src => src.MedicineLab))

                        // .ForMember(dest => dest.Medicines.MedicineLab,
                        // opt => opt.MapFrom(src => src.MedicineLab))

                        .ForPath(dest => dest.Medicines.MedicineDrug, opts => opts.MapFrom(src => src.MedicineDrug))

                           //        .ForMember(dest => dest.Medicines.MedicineDrug,
                           //  opt => opt.MapFrom(src => src.MedicineDrug))
                           .ForPath(dest => dest.Medicines.MedicineWeight, opts => opts.MapFrom(src => src.MedicineWeight))


                     /* .ForMember(dest => dest.Medicines.MedicineWeight,
              opt => opt.MapFrom(src => src.MedicineWeight))

                          .ForMember(dest => dest.Medicines.MedicineUnits,
              opt => opt.MapFrom(src => src.MedicineUnits));*/

                     .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.MedicinePicture))

                  .ForPath(dest => dest.Medicines.MedicineUnits, opts => opts.MapFrom(src => src.MedicineUnits))

                    .ForPath(dest => dest.LocationCityName, opts => opts.MapFrom(src => src.Estates.Locations.LocationCityName));


            CreateMap<MedicineForCreationDto, Medicines>();

            CreateMap<Resources_ForCreationDto, Medicines>();


            CreateMap<MedicineForUpdateDto, Medicines>()
                               .ForMember(a => a.MedicineQuantity, b => b.MapFrom(a => a.MedicineQuantity));
            
            CreateMap<Medicines, MedicineForUpdateDto>();

            CreateMap<Medicines, Resources_ForCreationDto>()
                 .ForPath(dest => dest.Quantity, opts => opts.MapFrom(src => src.MedicineQuantity))
                 .ForPath(dest => dest.Donation, opts => opts.MapFrom(src => src.MedicineDonation))
                 .ForPath(dest => dest.Name, opts => opts.MapFrom(src => src.MedicineName))
                 .ForPath(dest => dest.Availability, opts => opts.MapFrom(src => src.MedicineAvailability))
                 .ForPath(dest => dest.Picture, opts => opts.MapFrom(src => src.MedicinePicture))
                 .ForPath(dest => dest.Description, opts => opts.MapFrom(src => src.MedicineUtility))
                 .ForPath(dest => dest.FK_EstateID, opts => opts.MapFrom(src => src.FK_EstateID))

                 .ForPath(dest => dest.Medicines.MedicineExpirationDate, opts => opts.MapFrom(src => src.MedicineExpirationDate))
                 .ForPath(dest => dest.Medicines.MedicineLab, opts => opts.MapFrom(src => src.MedicineLab))
                 .ForPath(dest => dest.Medicines.MedicineDrug, opts => opts.MapFrom(src => src.MedicineDrug))
                 .ForPath(dest => dest.Medicines.MedicineWeight, opts => opts.MapFrom(src => src.MedicineWeight))
                 .ForPath(dest => dest.Medicines.MedicineUnits, opts => opts.MapFrom(src => src.MedicineUnits));
        }
    }
}
