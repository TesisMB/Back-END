using AutoMapper;
using Entities.DataTransferObjects.FormationsEstates___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class FormationsEstatesProfiles : Profile
    {
        public FormationsEstatesProfiles()
        {
            CreateMap<FormationsEstates, FormationsEstatesDto>();
        }
    }
}
