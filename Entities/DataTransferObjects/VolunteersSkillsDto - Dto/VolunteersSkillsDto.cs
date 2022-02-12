using Entities.DataTransferObjects.VolunteersSkillsFormationEstates;
using System.Collections.Generic;

namespace Back_End.Models.VolunteersSkills___Dto
{
    public class VolunteersSkillsDto
    {
        public int ID { get; set; }
        public string SkillName { get; set; }

        public ICollection<VolunteersSkillsFormationEstatesDto> VolunteersSkillsFormationEstates { get; set; }

    }
}
