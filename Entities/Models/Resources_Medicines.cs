
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
    [Table("Resources_Medicines", Schema = "dbo")]
    public class Resources_Medicines
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("FK_MedicineID")]
        public Medicines Medicines { get; set; }

        [Required]
        public int? FK_MedicineID { get; set; }

        [ForeignKey("FK_Resources_MedicinesID")]
        public ICollection<Resources_RequestResources_Materials_Medicines_Vehicles> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }

    }
}
