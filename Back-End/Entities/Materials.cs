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
        public int MaterialID { get; set; }

        [Required]
        [MaxLength(100)]
        public string MaterialName { get; set; }

        [Required]
        public int MaterialQuantity { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaterialLocation { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaterialUtility { get; set; }

        [Required]
        public bool MaterialAvailability { get; set; }

        [ForeignKey("EstateID")]
        public Estate Estate { get; set; }
        public int EstateID { get; set; }

    }
}
