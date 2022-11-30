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
        public string ID { get; set; }

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

        [Required]
        public Boolean MaterialAvailability { get; set; }

        [Required]

        public string MaterialPicture { get; set; }

        [Required]
        public Boolean MaterialDonation { get; set; }


        [Required]
        public int FK_EstateID { get; set; }


        public DateTime MaterialDateCreated { get; set; }

        public DateTime? MaterialDateModified { get; set; }


        [ForeignKey("FK_EstateID")]

        public Estates Estates { get; set; }


        public int CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }


        [ForeignKey("CreatedBy")]
        public Employees EmployeeCreated { get; set; }


        [ForeignKey("ModifiedBy")]
        public Employees? EmployeeModified { get; set; }

        public string Reason { get; set; }

        public bool Enabled { get; set; }

        [ForeignKey("FK_MaterialID")]
        public ICollection<ResourcesRequestMaterialsMedicinesVehicles> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }


    }
}
