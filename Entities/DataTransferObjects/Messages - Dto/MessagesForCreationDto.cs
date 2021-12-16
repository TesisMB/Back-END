using Entities.DataTransferObjects.CharRooms___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Messages___Dto
{
    public class MessagesForCreationDto
    {
        public int ID { get; set; }

        public String Message { get; set; }

        public Boolean MessageState { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        //public ChatRoomsForCreationDto ChatRooms { get; set; }

    }
}
