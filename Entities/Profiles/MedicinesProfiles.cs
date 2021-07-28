using AutoMapper;
using Entities.DataTransferObjects.Medicines___Dto;
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
            CreateMap<MedicineForCreationDto, Medicines>();


            CreateMap<MedicineForUpdateDto, Medicines>();
            CreateMap<Medicines, MedicineForUpdateDto>();
        }
    }
}
