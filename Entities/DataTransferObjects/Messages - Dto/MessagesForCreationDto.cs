using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;
using System;

namespace Entities.DataTransferObjects.Messages___Dto
{
    public class MessagesForCreationDto
    {
        public String message { get; set; }

        public Boolean? MessageState { get; set; } = false;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string chatRoomID { get; set; } 

        //public int? fK_LocationVolunteerID { get; set; }

        public int userID { get; set; }

        public string? Avatar { get; set; }
        public string? Name { get; set; }

        public DateMessageForCreationDto? DateMessage { get; set; }

    }

    public class SendMessage
    {
        public string message { get; set; }
        public Boolean MessageState { get; set; } = false;

        public string CreatedDate { get; set; }
        public int userID { get; set; }
        public string chatRoomID { get; set; }

        public string Avatar { get; set; }
        public string Name { get; set; }
    }
}
