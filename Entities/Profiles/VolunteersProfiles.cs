using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Volunteers__Dto;
using Entities.DataTransferObjects.Login___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.Volunteers__Dto;
using System.Linq;

namespace Back_End.Profiles
{
    public class VolunteersProfiles : Profile
    {
        public VolunteersProfiles()
        {
            CreateMap<Volunteers, ResourcesDto>()
              .ForMember(dest => dest.Description,
                            opt => opt.MapFrom(src => src.VolunteerDescription))

              .ForMember(dest => dest.Picture,
                            opt => opt.MapFrom(src => src.VolunteerAvatar))

              .ForPath(dest => dest.Volunteers.Users, opts => opts.MapFrom(src => src.Users))

              .ForPath(d => d.Volunteers.VolunteersSkills, o => o.MapFrom(s => s.VolunteersSkills))

              .ForPath(dest => dest.Estates, opts => opts.MapFrom(src => src.Users.Estates));


            CreateMap<Volunteers, VolunteersAppDto>();

            CreateMap<VolunteersForCreationDto, Volunteers>();
            CreateMap<VolunteersForUpdatoDto, Volunteers>();
            CreateMap<Volunteers, VolunteersForUpdatoDto>();

            CreateMap<Volunteers, VolunteersUserDto>();


        }
    }
}
