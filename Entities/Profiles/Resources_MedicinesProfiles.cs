using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class Resources_MedicinesProfiles : Profile
    {
        public Resources_MedicinesProfiles()
        {
            CreateMap<Resources_Medicines, Resources_MedicinesDto>();
            CreateMap<Resources_MedicinesForCreationDto, Resources_Medicines>();
            CreateMap<Resources_Medicines, Resources_MedicinesForCreationDto>();
        }
    }
}
