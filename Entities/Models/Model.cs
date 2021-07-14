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
        [Column("ID")]
        [Key]
        public int ModelID { get; set; }
       
        [Required]
        [MaxLength(50)]
        public string ModelName { get; set; }

        [Required]
        [ForeignKey("FK_MarkID")]
        public Marks Marks { get; set; }
        public int FK_MarkID { get; set; }

        [ForeignKey("FK_ModelID")]
        public ICollection<TypeVehiclesModels> MarksModels { get; set; }

    }
}
