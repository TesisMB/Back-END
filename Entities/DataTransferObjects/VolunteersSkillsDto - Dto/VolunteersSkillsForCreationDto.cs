using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.VolunteersSkillsFormationEstates;
using System.Collections.Generic;

namespace Back_End.Models.VolunteersSkillsDto___Dto
{
    public class VolunteersSkillsForCreationDto
    {
        public int FK_SkillID { get; set; }

        public SkillsForCreationDto Skills { get; set; }

        public ICollection<VolunteersSkillsFormationEstatesForCreationDto> VolunteersSkillsFormationEstates { get; set; }

    }
}
