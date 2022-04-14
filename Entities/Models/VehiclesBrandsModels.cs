using Back_End.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("VehiclesBrandsModels", Schema = "dbo")]

    public class VehiclesBrandsModels
    {

        [Key]

        public int ID { get; set; }

        [Required]
     
        public int FK_VehicleID { get; set; }

        public Vehicles Vehicles { get; set; }

        [Required]

        public int FK_BrandModelID { get; set; }

        public BrandsModels BrandsModels { get; set; }

    }
}
