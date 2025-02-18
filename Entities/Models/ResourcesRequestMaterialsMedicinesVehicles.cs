﻿using Back_End.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("ResourcesRequestMaterialsMedicinesVehicles", Schema = "dbo")]
    public class ResourcesRequestMaterialsMedicinesVehicles
    {
        [Key]
        public int ID { get; set; }

        public ResourcesRequest Resources_Request { get; set; }
        public int FK_Resource_RequestID { get; set; }

        public Materials Materials { get; set; }
        public string? FK_MaterialID { get; set; }
        

        public Medicines Medicines { get; set; }
        public string? FK_MedicineID { get; set; }

        public Vehicles Vehicles { get; set; }
        public string? FK_VehicleID { get; set; }

        [NotMapped]
        public string Estado { get; set; }
        public int Quantity { get; set; }
    }
}
