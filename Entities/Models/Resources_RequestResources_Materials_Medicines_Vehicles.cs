using Back_End.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Resources_RequestResources_Materials_Medicines_Vehicles", Schema="dbo")]
    public class Resources_RequestResources_Materials_Medicines_Vehicles
    {
        [Key]
        public int ID { get; set; }

        public Resources_Request Resources_Request { get; set; }
        public int FK_Resources_RequestID { get; set; }


        public Resources_Materials Resources_Materials { get; set; }
        public int? FK_Resources_MaterialsID { get; set; }

        public Resources_Medicines Resources_Medicines { get; set; }
        public int? FK_Resources_MedicinesID { get; set; }

        public Vehicles Vehicles { get; set; }
        public int? FK_VehiclesID { get; set; }
    }
}
