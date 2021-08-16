using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Locations" , Schema = "dbo")]
    public class Locations
    {
        [Key]
        [Column("ID")]
        public int LocationID { get; set; }

        [Required]
        [MaxLength(25)]
        public string LocationDepartmentName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LocationMunicipalityName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LocationCityName { get; set; }

        public string LocationLongitude { get; set; }

        public string LocationLatitude { get; set; }

        public ICollection<Estates> Estates { get; set; }

    }
}
