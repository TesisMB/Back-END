using Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
{
    [Table("Volunteers", Schema = "dbo")]
    public class Volunteers
    {

        [Key, ForeignKey("Users")]
        [Column("ID")]
        public int VolunteerID{ get; set; }
        public string VolunteerAvatar { get; set; }

        public string VolunteerDescription { get; set; }
        public Users Users { get; set; }

        public ICollection<VolunteersSkills> VolunteersSkills { get; set; }

        [ForeignKey("FK_VolunteerID")]
        public ICollection<VolunteersLocationVolunteersEmergenciesDisasters> VolunteersLocationVolunteersEmergenciesDisasters { get; set; }

    }
}
