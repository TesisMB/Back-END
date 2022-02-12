using AutoMapper;
using Back_End.Models;

namespace Back_End.Profiles
{
    public class RolesProfiles : Profile
    {
        public RolesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Roles, RolesDto>();
            CreateMap<RolesForCreationDto, Roles>();

        }
    }
}
