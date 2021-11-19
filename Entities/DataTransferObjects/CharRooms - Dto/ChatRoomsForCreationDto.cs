using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.CharRooms___Dto
{
    public class ChatRoomsForCreationDto
    {
        public int ID { get; set; }

        public DateTime CreateDate { get; set; }


        public TypesChatRoomsForCreationDto TypesChatRooms { get; set; }

    }
}
