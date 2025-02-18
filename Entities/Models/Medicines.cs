﻿using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Medicines", Schema = "dbo")]
    public class Medicines
    {
        [Key]
        public string ID { get; set; }

        [Required]
        [MaxLength(50)]
        public String MedicineName { get; set; }

        [Required]
        [MaxLength(50)]
        public int MedicineQuantity { get; set; }


        [Required]
        public DateTime MedicineExpirationDate { get; set; }

        [Required]
        [MaxLength(50)]
        public String MedicineLab { get; set; }

        [Required]
        [MaxLength(50)]
        public String MedicineDrug { get; set; }

        [Required]
        public double MedicineWeight { get; set; }

        [Required]
        public string MedicineUnits { get; set; }

        public String MedicineUtility { get; set; }

        [Required]
        public Boolean MedicineAvailability { get; set; }

        [Required]
        public string MedicinePicture { get; set; }

        [Required]
        public Boolean MedicineDonation { get; set; }

        [Required]
        public int FK_EstateID { get; set; }


        public DateTime MedicineDateCreated { get; set; }
        public DateTime? MedicineDateModified{ get; set; }

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


        [ForeignKey("FK_MedicineID")]
        public ICollection<ResourcesRequestMaterialsMedicinesVehicles> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }

    }
}
