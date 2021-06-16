using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class LocationAddressProfiles :Profile
    {
        public LocationAddressProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<LocationAddress, LocationAddressDto>()

                  .ForMember(dest => dest.LocationAddressID,
                                    opt => opt.MapFrom(src => src.ID));
        }
    }
}
