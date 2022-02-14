using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("ChatRooms", Schema = "dbo")]
    public class ChatRooms
    {
        [Key, ForeignKey("EmergenciesDisasters")]
        public int ID { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        [ForeignKey("FK_TypeChatRoomID")]
        public TypesChatRooms TypesChatRooms { get; set; }
        public int FK_TypeChatRoomID { get; set; }

        [ForeignKey("FK_ChatRoomID")]
        public ICollection<UsersChatRooms> UsersChatRooms { get; set; }

        public EmergenciesDisasters EmergenciesDisasters { get; set; }
        public ICollection<Messages> Messages { get; set; }

    }
}
