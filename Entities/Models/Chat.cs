using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Chat", Schema = "dbo")]
    public class Chat
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("FK_TypeChatRoomID")]
        public TypesChatRooms TypesChatRooms { get; set; }

        public int FK_TypeChatRoomID { get; set; }

        [ForeignKey("FK_ChatID")]
        public ICollection<UsersChat> UsersChat { get; set; }
    }
}
