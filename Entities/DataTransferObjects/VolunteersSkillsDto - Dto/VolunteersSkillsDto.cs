using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.VolunteersSkillsFormationEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.VolunteersSkills___Dto
{
    public class VolunteersSkillsDto
    {
        public int ID { get; set; }
        public string SkillName { get; set; }

        public ICollection<VolunteersSkillsFormationEstatesDto> VolunteersSkillsFormationEstates { get; set; }

    }
}
