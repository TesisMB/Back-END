using AutoMapper;
using Entities.DataTransferObjects.FormationsDates___Dto;
using Entities.DataTransferObjects.FormationsEstates___Dto;
using Entities.Helpers;
using Entities.Models;

namespace Entities.Profiles
{
    public class FormationsDatesProfiles: Profile
    {
        public FormationsDatesProfiles()
        {
            CreateMap<FormationsDates, FormationsDatesDto>()
                .ForMember(i => i.Date, opt => opt.MapFrom(src => DateTimeOffsetExtensions.GetDate(src.Date)));

            CreateMap<FormationsEstatesForCreationDto, FormationsDates>();



        }


    }
}
