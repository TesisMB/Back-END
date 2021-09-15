using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{

    [Table("MarksModels", Schema = "dbo")]
    public class MarksModels
    {
        [Key, ForeignKey("Vehicles")]
        public int ID { get; set; }

        [Required]
        public int FK_MarkID { get; set; }
        public Marks Marks { get; set; }

        [Required]
        public int FK_ModelID { get; set; }
        public Model Model { get; set; }

        public Vehicles Vehicles { get; set; }

    }
}
