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

        //public ICollection<ChatsDto> Chat { get; set; }

        //CHATROOMs
        public ICollection<ChatRoomsDto> ChatRooms { get; set; }



    }
}
