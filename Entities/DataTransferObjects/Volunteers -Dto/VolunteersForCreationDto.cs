using Back_End.Models.VolunteersSkillsDto___Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Back_End.Models.Employees___Dto
{
    public class VolunteersForCreationDto
    {
        public IFormFile ImageFile { get; set; }
        public string VolunteerAvatar { get; set; }

        public string VolunteerDescription { get; set; }

    }
}
