using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Volunteers__Dto;

namespace Back_End.Profiles
{
    public class VolunteersProfiles : Profile
    {
        public VolunteersProfiles()
        {
            CreateMap<Volunteers, VolunteersDto>();

            CreateMap<VolunteersForCreationDto, Volunteers>();
            CreateMap<VolunteersForUpdatoDto, Volunteers>();
            CreateMap<Volunteers, VolunteersForUpdatoDto>();
        }
    }
}
