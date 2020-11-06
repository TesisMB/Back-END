using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Entities
{
    [Table("Medicine", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Medicine
    {
        [Key]
        public int MedicineID { get; set; }

        [Required]
        [MaxLength(100)]
        public string MedicineName { get; set; }

        [Required]
        public int MedicineQuantity { get; set; }

        [Required]
        [MaxLength(50)]
        public string MedicineWeight { get; set; }

        [Required]
        [MaxLength(7)]
        public DateTimeOffset MedicineExpirationDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string MedicineLab { get; set; }

        [Required]
        [MaxLength(50)]
        public string MedicineDrug { get; set; }

        [ForeignKey("EstateID")]
        //public Estate Estate { get; set; }
        public int EstateID { get; set; }
    }
}
