using Back_End.Models.VolunteersSkills___Dto;
using System.Collections.Generic;

namespace Back_End.Models.Employees___Dto
{
    public class VolunteersDto
    {
        public string Dni { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //public VolunteersUserDto Users { get; set; }

        public IEnumerable<VolunteersSkillsDto> VolunteersSkills { get; set; }
    }
}
