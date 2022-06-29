using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("PDF", Schema="dbo")]
    public class PDF
    {
        [Key]
        public int ID { get; set; }

        public string Location { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Category { get; set; } = "Monitoreo";

        [NotMapped]
        public IFormFile LocationFile { get; set; }

    }
}
