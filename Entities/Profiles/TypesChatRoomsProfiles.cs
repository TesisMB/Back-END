using AutoMapper;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class TypesChatRoomsProfiles : Profile
    {
        public TypesChatRoomsProfiles()
        {
            CreateMap<TypesChatRooms, TypesChatsDto>();
              

            CreateMap<TypesChatRoomsForCreationDto, TypesChatRooms>();
        }
    }
}
