using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class PersonsForUpdatoDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
<<<<<<< Updated upstream
        public DateTime Birthdate { get; set; } 
=======
        public string Address { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public Boolean Available { get; set; }

>>>>>>> Stashed changes
        public Boolean Status { get; set; }
    }
}
