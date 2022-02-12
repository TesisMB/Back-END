using Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
{
    [Table("VolunteersSkills", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql

    public class VolunteersSkills
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("FK_VolunteerID")]
        public Volunteers Volunteers { get; set; }
        public int FK_VolunteerID { get; set; }

        [ForeignKey("FK_SkillID")]
        public Skills Skills { get; set; }
        public int FK_SkillID { get; set; }

        [ForeignKey("FK_VolunteerSkillID")]
        public ICollection<VolunteersSkillsFormationEstates> VolunteersSkillsFormationEstates { get; set; }
    }
}
