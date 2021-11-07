using Back_End.Models.VolunteersSkills___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Employees___Dto
{
    public class VolunteersDto
    {
        public VolunteersUsersDto Users { get; set; }

        public ICollection<VolunteersSkillsDto> VolunteersSkills { get; set; }
    }
}
