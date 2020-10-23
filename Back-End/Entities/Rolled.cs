using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Entities
{
    [Table("Rolled", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Rolled
    {
        [Key]
        public int RolledID { get; set; }

        [Required]
        public int RolledQuantity { get; set; }

        [Required]
        [MaxLength(30)]
        public string RolledBrand { get; set; }

        [Required]
        [MaxLength(20)]
        public string RolledModel{ get; set; }

        [Required]
        [MaxLength(30)]
        public string RolledName { get; set; }

        [Required]
        [MaxLength(50)]
        public string RolledKms { get; set; }

        [Required]
        public bool RolledIsAvailable { get; set; }

        [Required]
        [MaxLength(10)]
        public string RolledUtility { get; set; }

        [Required]
        [MaxLength(100)]
        public string RolledResponsible { get; set; }
    }
}
