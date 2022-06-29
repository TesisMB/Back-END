using AutoMapper;
using Entities.DataTransferObjects.PDF___Dto;
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
            CreateMap<PDF, PDFDto>();
        }
    }
}
