using Back_End.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
    [Table("Resources_Vehicles", Schema = "dbo")]
    public class Resources_Vehicles
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("FK_VehicleID")]
        public Vehicles Vehicles { get; set; }

        [Required]
        public int FK_VehicleID { get; set; }
    }
}
