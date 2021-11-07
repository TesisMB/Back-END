using AutoMapper;
using Entities.DataTransferObjects.FormationsDates___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class FormationsDatesProfiles: Profile
    {
        public FormationsDatesProfiles()
        {
            CreateMap<FormationsDates, FormationsDatesDto>();
        }
    }
}
