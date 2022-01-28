using Entities.DataTransferObjects.CharRooms___Dto;
using System;

namespace Entities.DataTransferObjects.Messages___Dto
{
    public class MessagesForCreationDto
    {
        public String message { get; set; }

        public Boolean MessageState { get; set; }

        public DateTime createdDate { get; set; } = DateTime.Now;

        public int FK_ChatRoomID { get; set; } 

        public int? FK_LocationVolunteerID { get; set; }

        public int FK_UserID { get; set; }

    }
}
