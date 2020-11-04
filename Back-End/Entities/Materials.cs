using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Entities
{
    [Table("Materials", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Materials
    {
        [Key]
        public int MaterialsID { get; set; }

        [Required]
        public int MaterialsQuantity { get; set; }

        [Required]
        [MaxLength(100)]
        public string MaterialsName { get; set; }

        [Required]
        public bool MaterialsIsAvailable { get; set; }

        [ForeignKey("EstateID")]
        public Estate Estate { get; set; }
        public int EstateID { get; set; }

        [Required]
        [MaxLength(30)]
        public string MaterialsUtility { get; set; }
    }
}
