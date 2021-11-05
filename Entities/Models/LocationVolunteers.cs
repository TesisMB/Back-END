using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("LocationVolunteers", Schema="dbo")]
    public class LocationVolunteers
    {
        public int ID { get; set; }

        public string LocationVolunteerLatitude { get; set; }

        public string LocationVolunteerLongitude { get; set; }
    }
}
