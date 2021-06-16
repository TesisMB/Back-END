using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    [Table("Skills", Schema = "dbo")]
    public class Skills
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string SkillName { get; set; }

        public string SkillDescription { get; set; }

        public string SkillIcon { get; set; }

        [ForeignKey("FK_SkillID")]

        public ICollection<VolunteersSkills> VolunteersSkills { get; set; }

    }
}
