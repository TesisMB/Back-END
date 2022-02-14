using Back_End.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("LocationVolunteers", Schema = "dbo")]
    public class LocationVolunteers
    {
        [Key, ForeignKey("Volunteers")]

        public int ID { get; set; }

        [Column(TypeName = "decimal(8, 6)")]
        public decimal? LocationVolunteerLatitude { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal? LocationVolunteerLongitude { get; set; }

        public Volunteers Volunteers { get; set; }
    }
}
