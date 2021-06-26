using Back_End.Models.Persons___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class PersonsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string Address { get; set; }
        public Boolean Available { get; set; }

    }
}
