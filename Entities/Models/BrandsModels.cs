using Back_End.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("BrandsModels", Schema = "dbo")]
    public class BrandsModels
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int FK_BrandID { get; set; }
        public Brands Brands { get; set; }

        [Required]
        public int FK_ModelID { get; set; }
        public Model Model { get; set; }


        [Required]
        public int FK_TypeVehicleID { get; set; }

        public TypeVehicles TypeVehicles { get; set; }

        //public Vehicles Vehicles { get; set; }

        [ForeignKey("FK_BrandModelID")]
        public ICollection<VehiclesBrandsModels> VehiclesBrandsModels { get; set; }


        //        [ForeignKey("FK_BrandModelID")]
        //public ICollection<VehiclesBrandsModels> VehiclesBrandsModels { get; set; }

    }
}
