using AutoMapper;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class ChatProfiles : Profile
    {
        public ChatProfiles()
        {
            CreateMap<Chat, ChatsDto>();

        }
    }
}
