using Back_End.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("PDF", Schema="dbo")]
    public class PDF
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public string Category { get; set; }


        public int FK_EmergencyDisasterID { get; set; }

        public DateTime? PDFDateModified { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }


        [ForeignKey("CreatedBy")]
        public Employees EmployeeCreated { get; set; }


        [ForeignKey("ModifiedBy")]
        public Employees? EmployeeModified { get; set; }

        [ForeignKey("FK_EmergencyDisasterID")]
        public EmergenciesDisasters EmergenciesDisasters { get; set; }

    }
}
