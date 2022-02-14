using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("VolunteersLocationVolunteersEmergenciesDisasters", Schema="dbo")]
    public class VolunteersLocationVolunteersEmergenciesDisasters
    {
        [Key]

        public int ID { get; set; }

        public Volunteers Volunteers { get; set; }
        public int FK_VolunteerID { get; set; }


        public LocationVolunteers LocationVolunteers { get; set; }
        public int FK_LocationVolunteerID { get; set; }


        public EmergenciesDisasters EmergenciesDisasters { get; set; }
        public int FK_EmergencyDisasterID { get; set; }
    }
}
