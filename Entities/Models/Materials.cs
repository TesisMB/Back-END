using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Materials", Schema = "dbo")]
    public class Materials
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaterialName { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaterialBrand { get; set; }

        [Required]
        [MaxLength(50)]
        public int MaterialQuantity { get; set; }

        public string MaterialUtility { get; set; }

        public Boolean MaterialAvailability { get; set; }

        public string MaterialPicture { get; set; }

        public int FK_EstateID { get; set; }

        [ForeignKey("FK_EstateID")]

        public Estates Estates { get; set; }

        [ForeignKey("FK_MaterialID")]
        public ICollection<ResourcesRequestMaterialsMedicinesVehicles> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }


    }
}
