using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
{
    [Table("Vehicles", Schema = "dbo")]
    public class Vehicles
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(9)]
        public string VehiclePatent { get; set; }

        [MaxLength(50)]
        public string VehicleUtility { get; set; }

        public string VehicleDescription { get; set; }

        [Required]
        public Boolean VehicleAvailability { get; set; }

        [Required]
        public string VehiclePicture { get; set; }

        [Required]
        public int VehicleQuantity{ get; set; }

        [Required]
        public string VehicleYear { get; set; }

        [Required]
        public Boolean VehicleDonation { get; set; }


        [ForeignKey("FK_EstateID")]
        public Estates Estates { get; set; }
        public int? FK_EstateID { get; set; }

        [ForeignKey("FK_EmployeeID")]
        public Employees Employees { get; set; }
        public int? FK_EmployeeID { get; set; }

        [ForeignKey("Fk_TypeVehicleID")]
        public TypeVehicles TypeVehicles { get; set; }
        public int Fk_TypeVehicleID { get; set; }


        [Required]
        [ForeignKey("FK_BrandID")]
        public Brands Brands { get; set; }
        public int FK_BrandID { get; set; }

        [Required]
        [ForeignKey("FK_ModelID")]
        public Model Model { get; set; }
        public int FK_ModelID { get; set; }


        //public BrandsModels BrandsModels { get; set; }

        [ForeignKey("FK_VehicleID")]
        public ICollection<ResourcesRequestMaterialsMedicinesVehicles> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }

    }
}
