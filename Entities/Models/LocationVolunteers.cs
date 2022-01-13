using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("LocationVolunteers", Schema = "dbo")]
    public class LocationVolunteers
    {
        public int ID { get; set; }

        [Column(TypeName = "decimal(8, 6)")]
        public decimal LocationVolunteerLatitude { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal LocationVolunteerLongitude { get; set; }

        public Messages Messages { get; set; }
    }
}
