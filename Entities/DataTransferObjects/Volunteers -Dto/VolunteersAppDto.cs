using Back_End.Models.VolunteersSkills___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Volunteers__Dto
{
    public class VolunteersAppDto
    {
        public int VOlunteerID { get; set; }

        public string VolunteerAvatar { get; set; }
        public VolunteersUsersAppDto Users { get; set; }

        public ICollection<VolunteersSkillsDto> VolunteersSkills { get; set; }

    }
}
