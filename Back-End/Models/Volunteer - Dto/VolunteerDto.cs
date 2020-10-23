using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.VolunteerDto
{
    public class VolunteerDto
    {
        public int VolunteerID { get; set; }

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
