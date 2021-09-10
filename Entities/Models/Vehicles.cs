using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    [Table("Vehicles", Schema = "dbo")]
    public class Vehicles
    {
       
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(9)]
        public string VehiclePatent { get; set; }

        [MaxLength(50)]
        public string VehicleUtility { get; set; }

        public string VehicleDescription { get; set; }

        [Required]
        public Boolean VehicleAvailability { get; set; }

        [Required]
        public string VehiclePicture { get; set; }

        [ForeignKey("FK_EstateID")]
        public Estates Estates { get; set; }
        public int? FK_EstateID { get; set; }

        [ForeignKey("FK_EmployeeID")]
        public Employees Employees { get; set; }
        public int? FK_EmployeeID { get; set; }

        [ForeignKey("Fk_TypeVehicleID")]
        public TypeVehicles Type{ get; set; }
        public int Fk_TypeVehicleID { get; set; }
    }
}
