using Back_End.Models.VolunteersSkillsDto___Dto;
using System;
using System.Collections.Generic;

namespace Back_End.Models.Employees___Dto
{
    public class VolunteersForCreationDto
    {
        public string UserDni { get; set; }
        public string UserPassword { get; set; }

        public int FK_RoleID { get; set; }

        public string ResetToken { get; set; } = null;

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public PersonForCreationDto Persons { get; set; }

        public ICollection<VolunteersSkillsForCreationDto> VolunteersSkills { get; set; }

    }
}
