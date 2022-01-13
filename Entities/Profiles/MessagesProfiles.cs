using AutoMapper;
using Entities.DataTransferObjects.Messages___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class MessagesProfiles : Profile
    {
        public MessagesProfiles()
        {
            CreateMap<Messages, MessagesDto>();

            CreateMap<Messages, MessagesForCreationDto>();
            CreateMap<MessagesForCreationDto, Messages>();
        }
    }
}
