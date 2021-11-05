using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Messages", Schema="dbo")]
    public class Messages
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public String Message { get; set; }

        [Required]
        public Boolean MessageState { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        [ForeignKey("FK_ChatRoomID")]
        public ChatRooms ChatRooms { get; set; }

        public int FK_ChatRoomID { get; set; }

        [ForeignKey("FK_LocationVolunteerID")]
        public LocationVolunteers LocationVolunteers { get; set; }
        public int FK_LocationVolunteerID { get; set; }
    }
}
