using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Entities
{
    [Table("Rolled", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Vehicles
    {
        [Key]
        public int VehicleID { get; set; }

        [Required]
        public int VehicleQuantity { get; set; }

        [Required]
        [MaxLength(10)]
        public string VehicleMark { get; set; }

        [Required]
        [MaxLength(25)]
        public string VehicleModel { get; set; }

        [Required]
        [MaxLength(30)]
        public string VehiclePatent { get; set; }

        [Required]
        [MaxLength(50)]
        public string VehicleKms { get; set; }

        [Required]
        [MaxLength(50)]
        public string VehicleUtility { get; set; }

        [Required]
        [MaxLength(50)]
        public string VehicleManager { get; set; }

        [Required]
        public bool VehicleAvailability{ get; set; }

    }
}
