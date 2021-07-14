using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Marks", Schema = "dbo")]
    public class Marks
    {
        [Column("ID")]
        [Key]
        public int MarkID { get; set; }

        [Required]
        [MaxLength(50)]
        public string MarkName { get; set; }
    }
}
