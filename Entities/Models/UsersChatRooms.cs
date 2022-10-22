using Back_End.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("UsersChatRooms", Schema = "dbo")]
    public class UsersChatRooms
    {
        [Key]
        public int ID { get; set; }

        public int FK_UserID { get; set; }

        public int FK_ChatRoomID { get; set; }
        public bool Status { get; set; }

        [ForeignKey("FK_UserID")]
        public Users Users { get; set; }

        [ForeignKey("FK_ChatRoomID")]
        public ChatRooms Chat { get; set; }
    }
}
