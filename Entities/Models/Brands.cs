using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Brands", Schema = "dbo")]
    public class Brands
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string BrandName { get; set; }

        [ForeignKey("FK_BrandID")]
        public ICollection<BrandsModels> MarksModels { get; set; }

    }
}
