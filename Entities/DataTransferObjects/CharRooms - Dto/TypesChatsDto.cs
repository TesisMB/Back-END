using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.CharRooms___Dto
{
    public class TypesChatsDto
    {
        //TYPES - CHATROOM
        public int ID { get; set; }

        public Boolean IsGroupChat { get; set; }

        public ICollection<ChatsDto> Chat { get; set; }

        //CHATROOM - 1 - 1
        public ICollection<ChatRoomsDto> ChatRooms { get; set; }



    }
}
