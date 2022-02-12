using System;

namespace Back_End.Models
{
    public class VolunteersUsersDto
    {
        public DateTime? Birthdate { get; set; }
        public string Address { get; set; }
        public Boolean Status { get; set; }

        //public PersonsDto Persons { get; set; }
        //public LocationsDto Locations { get; set; }
    }
}
