using AutoMapper;
using Entities.DataTransferObjects.Medicines___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class MedicinesProfiles : Profile
    {

        public MedicinesProfiles()
        {
            CreateMap<Medicines, MedicinesDto>();

            CreateMap<Medicines, Resources_Dto>()

                .ForMember(dest => dest.Name,
                            opt => opt.MapFrom(src => src.MedicineName))

                  .ForMember(dest => dest.Quantity,
                            opt => opt.MapFrom(src => src.MedicineQuantity))

                    .ForMember(dest => dest.Description,
                            opt => opt.MapFrom(src => src.MedicineUtility))

                      .ForMember(dest => dest.Availability,
                            opt => opt.MapFrom(src => src.MedicineAvailability))

                     .ForPath(dest => dest.Medicines.MedicineExpirationDate, opts => opts.MapFrom(src => src.MedicineExpirationDate))

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


            CreateMap<MedicineForUpdateDto, Medicines>();
            CreateMap<Medicines, MedicineForUpdateDto>();
        }
    }
}
