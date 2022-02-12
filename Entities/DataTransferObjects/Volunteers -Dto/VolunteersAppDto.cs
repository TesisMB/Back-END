using Back_End.Models.VolunteersSkills___Dto;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.Volunteers__Dto
{
    public class VolunteersAppDto
    {
        public int ID { get; set; }

        public string VolunteerAvatar { get; set; }
        public VolunteersUsersAppDto UsersVolunteers { get; set; }

        public ICollection<VolunteersSkillsDto> VolunteersSkills { get; set; }

    }
}
