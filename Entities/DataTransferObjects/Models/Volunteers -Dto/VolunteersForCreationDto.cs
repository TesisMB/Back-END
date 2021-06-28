using Back_End.Models.Users___Dto;
using Back_End.Models.VolunteersSkillsDto___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Employees___Dto
{
    public class VolunteersForCreationDto
    {
        public string VolunteerAvatar { get; set; }

        public string VolunteerDescription { get; set; }

        public UsersVolunteersForCreationDto Users { get; set; }
        public ICollection<VolunteersSkillsForCreationDto> VolunteersSkills { get; set; }

    }
}
