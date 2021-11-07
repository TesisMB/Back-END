using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("ChatRooms", Schema="dbo")]
    public class ChatRooms
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        [ForeignKey("TypeChatRoomID")]
        public TypesChatRooms TypesChatRooms { get; set; }
        public int TypeChatRoomID { get; set; }

        [ForeignKey("FK_ChatRoomID")]
        public ICollection<UsersChatRooms> UsersChatRooms { get; set; }

    }
}
