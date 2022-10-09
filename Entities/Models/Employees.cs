using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Back_End.Models
{
    [Table("Employees", Schema = "dbo")]
    public class Employees
    {
        [Key, ForeignKey("Users")]
        [Column("ID")]
        public int EmployeeID { get; set; }

        [Required]
        public DateTime EmployeeCreatedate { get; set; }

        public Users Users { get; set; }

        public ICollection<Vehicles> Vehicles { get; set; }

        public ICollection<EmergenciesDisasters> EmergenciesDisasters { get; set; }
        public ICollection<EmergenciesDisasters> EmergenciesDisastersModified { get; set; }
        public ICollection<EmergenciesDisasters> EmergenciesDisastersCreated { get; set; }

        public ICollection<Vehicles> VehicleModified { get; set; }
        public ICollection<Vehicles> VehicleCreated { get; set; }


        public ICollection<Medicines> MedicinesModified { get; set; }
        public ICollection<Medicines> MedicinesCreated { get; set; }


        public ICollection<Materials> MaterialsModified { get; set; }
        public ICollection<Materials> MaterialsCreated { get; set; }


        public ICollection<ResourcesRequest> ResourcesModified { get; set; }
        public ICollection<ResourcesRequest> ResourcesCreated { get; set; }
        public ICollection<ResourcesRequest> ResourcesResponse { get; set; }


        public ICollection<PDF> PDFModified { get; set; }
        public ICollection<PDF> PDFCreated { get; set; }
    }
}
