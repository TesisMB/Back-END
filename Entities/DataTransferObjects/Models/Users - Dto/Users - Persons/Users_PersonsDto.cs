using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Users___Dto.Users___Persons
{
    public class Users_PersonsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public Boolean Status { get; set; }
        public DateTimeOffset Birthdate { get; set; }
    }
}
