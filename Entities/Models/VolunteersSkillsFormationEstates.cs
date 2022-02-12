using Back_End.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("VolunteersSkillsFormationEstates", Schema = "dbo")]
    public class VolunteersSkillsFormationEstates
    {
        [Required]
        public int FK_VolunteerSkillID { get; set; }
        public VolunteersSkills VolunteersSkills { get; set; }

        [Required]
        public int FK_FormationEstateID { get; set; }
        public FormationsEstates FormationsEstates { get; set; }
    }
}
