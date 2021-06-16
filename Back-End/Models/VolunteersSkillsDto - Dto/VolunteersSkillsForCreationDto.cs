using Back_End.Models.Employees___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.VolunteersSkillsDto___Dto
{
    public class VolunteersSkillsForCreationDto
    {
        public int FK_SkillID { get; set; }

        public SkillsForCreationDto Skills { get; set; }
    }
}
