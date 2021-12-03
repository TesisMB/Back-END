using Back_End.Models.VolunteersSkills___Dto;
using Entities.DataTransferObjects.Volunteers__Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Employees___Dto
{
    public class VolunteersDto
    {
        public string Dni { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
   
        //public VolunteerUserDto Users { get; set; }

        // public ICollection<VolunteersSkillsDto> VolunteersSkills { get; set; }
    }
}
