using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.CharRooms___Dto
{
   public class ChatsDto
    {
        public int ID { get; set; }
        public ICollection<UsersChatDto> UsersChat { get; set; }

    }
}
