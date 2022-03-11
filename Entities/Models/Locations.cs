using Back_End.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Locations", Schema = "dbo")]
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

        [Column(TypeName = "decimal(8, 6)")]
        public decimal LocationLongitude { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal LocationLatitude { get; set; }

        public ICollection<Estates> Estates { get; set; }

        public ICollection<EmergenciesDisasters> EmergenciesDisasters { get; set; }

    }
}
