using AutoMapper;
using Back_End.Models;
using Back_End.Models.Users___Dto.Users___Persons;

namespace Back_End.Profiles
{
    public class PersonsProfiles:Profile
    {
        public PersonsProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Persons, PersonsDto>();
            CreateMap<Persons, Users_PersonsDto>()

                .ForMember(dest => dest.Status,
                                    opt => opt.MapFrom(src => src.Available));

            CreateMap<PersonForCreationDto, Persons>();

            CreateMap<PersonsForUpdatoDto, Persons>();
            CreateMap<Persons, PersonsForUpdatoDto>();


            CreateMap<UserAuthDto, Persons>();

        }
    }
}
