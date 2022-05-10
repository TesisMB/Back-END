using System;


namespace Entities.DataTransferObjects.CharRooms___Dto
{
    public class ChatRoomsForCreationDto
    {

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public int FK_TypeChatRoomID { get; set; }


    }
}
