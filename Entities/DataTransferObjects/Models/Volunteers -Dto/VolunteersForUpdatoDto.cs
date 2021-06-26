using Back_End.Models.VolunteersSkillsDto___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Volunteers__Dto
{
    public class VolunteersForUpdatoDto
    {
        public string VolunteerAvatar { get; set; }

        public string VolunteerDescription { get; set; }
        public UsersForUpdateDto Users { get; set; }
    }
}
