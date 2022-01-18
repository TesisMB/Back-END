using System;

namespace Entities.DataTransferObjects.Messages___Dto
{
    public class MessagesForCreationDto
    {
        public String Message { get; set; }

        public Boolean MessageState { get; set; }

        public DateTime CreatedDate { get; set; }

        public int FK_ChatRoomID { get; set; } 

        public int? FK_LocationVolunteerID { get; set; }


        //public ChatRoomsForCreationDto ChatRooms { get; set; }

    }
}
