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
        [Key, ForeignKey("EmergenciesDisasters")]
        public int ID { get; set; }

        public string Location { get; set; }
        public DateTime CreateDate { get; set; }
        public string Category { get; set; }

        public EmergenciesDisasters EmergenciesDisasters { get; set; }

    }
}
