using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("FormationsEstates", Schema = "dbo")]
    public class FormationsEstates
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FormationEstateName { get; set; }

        [ForeignKey("FK_FormationDateID")]
        public FormationsDates FormationsDates { get; set; }

        [Required]
        public int FK_FormationDateID { get; set; }


        [ForeignKey("FK_FormationEstateID")]
        public ICollection<VolunteersSkillsFormationEstates> VolunteersSkillsFormationEstates { get; set; }
    }
}
