using Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
{
    [Table("TypeVehicles", Schema = "dbo")]
    public class TypeVehicles
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(25)]
        public string Type { get; set; }


        [ForeignKey("FK_TypeVehicleID")]
        public ICollection<BrandsModels> BrandModels { get; set; }

        // public ICollection<Vehicles> Vehicles { get; set; }

    }
}
