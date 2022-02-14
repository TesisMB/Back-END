using Back_End.Models.VolunteersSkills___Dto;
using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.Volunteers__Dto
{
    public class VolunteersAppDto
    {
        public int ID { get; set; }

        public string VolunteerAvatar { get; set; }

        public string UserDni { get; set; }

        public string Name { get; set; }
        public Boolean Status { get; set; }

        //public VolunteersUsersAppDto UsersVolunteers { get; set; }

        public ICollection<VolunteersSkillsDto> VolunteersSkills { get; set; }

    }
}
