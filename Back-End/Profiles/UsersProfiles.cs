using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Profiles
{
    public class UsersProfiles : Profile
    {
        public UsersProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Users, UsersDto>()

                .ForMember(dest => dest.RoleName,
                                    opt => opt.MapFrom(src => src.Roles.RoleName))

                .ForMember(dest => dest.UserID,
                                    opt => opt.MapFrom(src => src.ID))

                .ForMember(dest => dest.UserAvailable,
                                    opt => opt.MapFrom(src => src.UserAvailability));

            CreateMap<UsersForCreationDto, Users>();

            CreateMap<UsersForUpdate, Users>();



        }
    }
}



