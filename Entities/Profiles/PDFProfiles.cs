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
                        .ForMember(i => i.CreateDate, opt => opt.MapFrom(src => DateTimeOffsetExtensions.GetDate2(src.CreateDate)));
        }
    }
}
