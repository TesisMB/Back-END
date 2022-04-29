using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("LocationsEmergenciesDisasters", Schema = "dbo")]
    public class LocationsEmergenciesDisasters
    {
        [Key, ForeignKey("EmergenciesDisasters")]
        
        public int ID { get; set; }

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

        public EmergenciesDisasters EmergenciesDisasters { get; set; }
    }
}
