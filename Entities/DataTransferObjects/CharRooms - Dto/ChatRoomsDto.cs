using Entities.DataTransferObjects.Messages___Dto;
using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.CharRooms___Dto
{
    public class ChatRoomsDto
    {
        public int ID { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<UsersChatRoomsDto> UsersChatRooms { get; set; }

    }
}
