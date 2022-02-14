using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
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

        public ICollection<VolunteersSkills> VolunteersSkills { get; set; }
    }
}
