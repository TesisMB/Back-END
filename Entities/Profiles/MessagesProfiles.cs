using AutoMapper;
using Entities.DataTransferObjects.Messages___Dto;
using Entities.Helpers;
using Entities.Models;

namespace Entities.Profiles
{
    public class MessagesProfiles : Profile
    {
        public MessagesProfiles()
        {
            CreateMap<Messages, MessagesDto>()
                .ForPath(i => i.Name, opt => opt.MapFrom(src => src.Users.Persons.FirstName))
                .ForMember(i => i.CreatedDate, opt => opt.MapFrom(src => DateTimeOffsetExtensions.GetTime(src.CreatedDate)));

            CreateMap<Messages, MessagesForCreationDto>();
            
            CreateMap<MessagesForCreationDto, Messages>();
        }
    }
}
