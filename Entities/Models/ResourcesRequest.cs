using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("ResourcesRequest", Schema = "dbo")]
    public class ResourcesRequest
    {

    
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        public string Description { get; set; }

        [Required]
        public Boolean Status { get; set; }

        [Required]
        public string Condition { get; set; }

        public string Reason { get; set; }

        [ForeignKey("FK_UserID")]
        public Users Users { get; set; }


        [ForeignKey("FK_EmergencyDisasterID")]
        public EmergenciesDisasters EmergenciesDisasters { get; set; }

        [Required]
        public int FK_UserID { get; set; }

        [Required]
        public int FK_EmergencyDisasterID { get; set; }

        [ForeignKey("FK_Resource_RequestID")]
        public ICollection<ResourcesRequestMaterialsMedicinesVehicles> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }

        //public Resources  Resources { get; set; }
    }
}
