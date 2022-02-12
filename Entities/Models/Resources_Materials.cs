using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
    [Table("Resources_Materials", Schema = "dbo")]
    public class Resources_Materials
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("FK_MaterialID")]
        public Materials Materials { get; set; }

        [Required]
        public int FK_MaterialID { get; set; }

        [ForeignKey("FK_Resources_MaterialsID")]
        public ICollection<Resources_RequestResources_Materials_Medicines_Vehicles> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }
    }

}
