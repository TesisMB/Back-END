using Back_End.Models.VolunteersSkills___Dto;
using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.Volunteers__Dto
{
    public class VolunteerDto
    {
        public string Dni { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Address { get; set; }

        public string CreatedDate { get; set; }
        public ICollection<VolunteersSkillsDto> VolunteersSkills { get; set; }

    }
}
