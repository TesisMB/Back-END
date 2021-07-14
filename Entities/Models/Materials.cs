using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table ("Materials", Schema = "dbo")]
   public class Materials
    {
        [Key]
        [Column("ID")]
        public int MaterialID { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaterialName { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaterialMark { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaterialQuantity { get; set; }

        public string MaterialUtility { get; set; }

        public Boolean MaterialAvailability { get; set; }

        public string MaterialPicture { get; set; }

        public int FK_EstateID { get; set; }

        [ForeignKey("FK_EstateID")]

        public Estates Estates { get; set; }

    }
}
