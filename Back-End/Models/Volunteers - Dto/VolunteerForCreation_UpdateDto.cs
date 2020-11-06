using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerForCreation_UpdateDto
    {
        public string VolunteerFirstName { get; set; }

        public string VolunteerLastName { get; set; }

        public string VolunteerDni { get; set; }

        public string VolunteerAddress { get; set; }

        public string VolunteerPhone { get; set; }

        public string VolunteerEmail { get; set; }

        public string VolunteerSpecialty { get; set; }

        public string VolunteerAvatar { get; set; }
    }
}
