using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;
using System;

namespace Entities.DataTransferObjects.Messages___Dto
{
    public class MessagesForCreationDto
    {
        public String Message { get; set; }

        public Boolean MessageState { get; set; } = false;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public int FK_ChatRoomID { get; set; } 

        //public int? fK_LocationVolunteerID { get; set; }

        public int FK_UserID { get; set; }

        public string Avatar { get; set; }
        public string Name { get; set; }

        public DateMessageForCreationDto DateMessage { get; set; }


    }
}
