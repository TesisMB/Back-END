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
                  .ForMember(dest => dest.userID,
                                opt => opt.MapFrom(src => src.FK_UserID))

                .ForPath(i => i.Name, opt => opt.MapFrom(src => src.Users.Persons.FirstName));

            CreateMap<Messages, MessagesForCreationDto>();
            
            CreateMap<MessagesForCreationDto, Messages>();
        }
    }
}
