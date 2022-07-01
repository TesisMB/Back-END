using AutoMapper;
using Entities.DataTransferObjects.PDF___Dto;
using Entities.Helpers;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Profiles
{
    public class PDFProfiles : Profile
    {
        public PDFProfiles()
        {
            CreateMap<PDF, PDFDto>()
                        .ForMember(i => i.CreateDate, opt => opt.MapFrom(src => DateTimeOffsetExtensions.GetDate2(src.CreateDate)))
                        .ForMember(i => i.PDFDateModified, opt => opt.MapFrom(src => DateTimeOffsetExtensions.GetDate2(src.PDFDateModified)))

                            .ForMember(dest => dest.CreatedByEmployee,
                                       opt => opt.MapFrom(src => src.EmployeeCreated.Users.Persons.FirstName + " " + src.EmployeeCreated.Users.Persons.LastName))

                           .ForMember(dest => dest.ModifiedByEmployee,
                                       opt => opt.MapFrom(src => src.EmployeeModified.Users.Persons.FirstName + " " + src.EmployeeModified.Users.Persons.LastName));


            CreateMap<PDFForCreationDto, PDF>();

            CreateMap<PDF, PDFForUpdateDto>();
            CreateMap<PDFForUpdateDto, PDF>();

        }
    }
}
