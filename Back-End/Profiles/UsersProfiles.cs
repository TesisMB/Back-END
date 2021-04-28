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
            CreateMap<Entities.Users, Models.UsersDto>()

                 .ForMember(dest => dest.RoleID,
                                    opt => opt.MapFrom(src => src.FK_RoleID))

                .ForMember(dest => dest.RoleName,
                                    opt => opt.MapFrom(src => src.Roles.RoleName));

            CreateMap<UsersForCreationDto, Users>();

            CreateMap<UsersForCreationDto, Persons>();
        }
    }
}



