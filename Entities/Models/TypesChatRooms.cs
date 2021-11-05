using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("TypesChatRooms", Schema="dbo")]
    public class TypesChatRooms
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public String Name { get; set; }

        public ICollection<ChatRooms> ChatRooms { get; set; }
    }
}
