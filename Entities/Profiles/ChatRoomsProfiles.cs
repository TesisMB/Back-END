using AutoMapper;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Helpers;
using Entities.Models;


namespace Entities.Profiles
{
    public class ChatRoomsProfiles : Profile
    {
        public ChatRoomsProfiles()
        {
            CreateMap<ChatRoomsForCreationDto, ChatRooms>();



            CreateMap<ChatRooms, ChatRoomsDto>();
        }
    }
}
