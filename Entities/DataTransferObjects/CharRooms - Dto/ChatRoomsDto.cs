using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.CharRooms___Dto
{
   public class ChatRoomsDto
    {
        public int ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<UsersChatRoomsDto> UsersChatRooms { get; set; }

        public ChatRoomsEmergenciesDisastersDto EmergenciesDisasters { get; set; }
    }
}
