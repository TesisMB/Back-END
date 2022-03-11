using Back_End.Models.Users___Dto;
using Back_End.Models.VolunteersSkillsDto___Dto;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Back_End.Models.Employees___Dto
{
    public class VolunteersForCreationDto
    {
        public string VolunteerAvatar { get; set; }
        public IFormFile ImageFile { get; set; }

        public string VolunteerDescription { get; set; }

        public ICollection<VolunteersSkillsForCreationDto> VolunteersSkills { get; set; }

    }
}
