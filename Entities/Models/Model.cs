using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Model", Schema = "dbo")]
    public class Model
    {
        [Key]
        public int ID { get; set; }
       
        [Required]
        [MaxLength(50)]
        public string ModelName { get; set; }

        [ForeignKey("FK_ModelID")]
        public ICollection<BrandsModels> MarksModels { get; set; }

    }
}
