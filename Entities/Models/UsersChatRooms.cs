using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("UsersChatRooms", Schema="dbo")]
    public class UsersChatRooms
    {
        public Users Users { get; set; }

        public ChatRooms chatRooms { get; set; }
        public int Fk_UserID { get; set; }
        public int FK_ChatRoomID { get; set; }
    }
}
