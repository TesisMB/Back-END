using AutoMapper;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;


namespace Entities.Profiles
{
    public class ChatRoomsProfiles : Profile
    {
        public ChatRoomsProfiles()
        {
            CreateMap<ChatRoomsForCreationDto, ChatRooms>();
            CreateMap<ChatRooms, ChatRoomsDto>()
                .ForPath(src => src.ChatRoomID, a => a.MapFrom(i => i.ID));
        }
    }
}
