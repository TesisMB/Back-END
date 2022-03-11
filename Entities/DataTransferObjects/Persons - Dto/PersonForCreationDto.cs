using System;

namespace Back_End.Models
{
    public class PersonForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }
        public Boolean Status { get; set; } = true;

        public string LocationName { get; set; }

    }
}
