using Entities.DataTransferObjects.CharRooms___Dto;
using System;

namespace Entities.DataTransferObjects.Messages___Dto
{
    public class MessagesForCreationDto
    {
        public String message { get; set; }

        public Boolean messageState { get; set; }

        public DateTime createdDate { get; set; } = DateTime.Now;

        public int fK_ChatRoomID { get; set; } 

        public int? fK_LocationVolunteerID { get; set; }

        public int fK_UserID { get; set; }

    }
}
