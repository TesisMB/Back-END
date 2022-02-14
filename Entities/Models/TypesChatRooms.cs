using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("TypesChatRooms", Schema = "dbo")]
    public class TypesChatRooms
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Boolean IsGroupChat { get; set; }

        public ICollection<Chat> Chat { get; set; }
        public ICollection<ChatRooms> ChatRooms { get; set; }
    }
}
