using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.CharRooms___Dto
{
    public class ChatRoomsEmergenciesDisastersDto
    {
        public int ID { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<UsersChatRoomsEmergenciesDisastersDto> UsersChatRooms { get; set; }
    }
}
