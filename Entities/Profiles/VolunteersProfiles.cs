using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Volunteers__Dto;
using Entities.DataTransferObjects.Login___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.Volunteers__Dto;

namespace Back_End.Profiles
{
    public class VolunteersProfiles : Profile
    {
        public VolunteersProfiles()
        {
            CreateMap<Volunteers, Resources_Dto>()
              .ForMember(dest => dest.Description,
                            opt => opt.MapFrom(src => src.VolunteerDescription))


               .ForMember(dest => dest.Name,
                            opt => opt.MapFrom(src => $"{src.Users.Persons.FirstName} {src.Users.Persons.LastName}"))

              .ForMember(dest => dest.Picture,
                            opt => opt.MapFrom(src => src.VolunteerAvatar))

              //.ForPath(dest => dest.Volunteers.Users, opts => opts.MapFrom(src => src.Users))

              .ForPath(d => d.Volunteers.VolunteersSkills, o => o.MapFrom(s => s.VolunteersSkills))


              .ForPath(dest => dest.Estates, opts => opts.MapFrom(src => src.Users.Estates))


              .ForPath(dest => dest.LocationCityName, opts => opts.MapFrom(src => src.Users.Estates.Locations.LocationCityName))

              .ForMember(dest => dest.Availability,
                            opt => opt.MapFrom(src => src.Users.Persons.Status))

               .ForPath(dest => dest.Volunteers.Email, opts => opts.MapFrom(src => src.Users.Persons.Email))

               .ForPath(dest => dest.Volunteers.Phone, opts => opts.MapFrom(src => src.Users.Persons.Phone))

               .ForPath(dest => dest.Volunteers.Dni, opts => opts.MapFrom(src => src.Users.UserDni));


            CreateMap<Volunteers, Resource_Dto>()
             .ForMember(dest => dest.Description,
                        opt => opt.MapFrom(src => src.VolunteerDescription))


           .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => $"{src.Users.Persons.FirstName} {src.Users.Persons.LastName}"))

          .ForMember(dest => dest.Picture,
                        opt => opt.MapFrom(src => src.VolunteerAvatar))

           .ForPath(dest => dest.Volunteers.Email, opts => opts.MapFrom(src => src.Users.Persons.Email))

           .ForPath(dest => dest.Volunteers.Phone, opts => opts.MapFrom(src => src.Users.Persons.Phone))

           .ForPath(dest => dest.Volunteers.Dni, opts => opts.MapFrom(src => src.Users.UserDni))

            .ForPath(dest => dest.Volunteers.Birthdate, opts => opts.MapFrom(src => src.Users.Persons.Birthdate))

           .ForPath(dest => dest.Volunteers.Address, opts => opts.MapFrom(src => src.Users.Persons.Address))

           .ForMember(dest => dest.Availability,
                        opt => opt.MapFrom(src => src.Users.Persons.Status))

          .ForPath(d => d.Volunteers.VolunteersSkills, o => o.MapFrom(s => s.VolunteersSkills))

          .ForPath(dest => dest.LocationCityName, opts => opts.MapFrom(src => src.Users.Estates.Locations.LocationCityName));


            CreateMap<Volunteers, VolunteersAppDto>()
           .ForPath(dest => dest.VolunteersSkills, opts => opts.MapFrom(src => src.VolunteersSkills))
           .ForPath(dest => dest.UsersVolunteers, opts => opts.MapFrom(src => src.Users));

            CreateMap<VolunteersForCreationDto, Volunteers>();
            CreateMap<VolunteersForUpdatoDto, Volunteers>();
            CreateMap<Volunteers, VolunteersForUpdatoDto>();

            CreateMap<Volunteers, VolunteersUserDto>();


        }
    }
}
