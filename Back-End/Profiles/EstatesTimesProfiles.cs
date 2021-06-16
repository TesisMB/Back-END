using AutoMapper;
using Back_End.Entities;
using Back_End.Models.Employees___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class EstatesTimesProfiles: Profile
    {
        public EstatesTimesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<EstatesTimes, EstatesTimesDto>();
        }
    }
}
