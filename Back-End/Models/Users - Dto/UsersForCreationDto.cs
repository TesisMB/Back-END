using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class UsersForCreationDto // : UsersForManipulationDto
    {
        public string UserDni { get; set; }
        public string UserPassword { get; set; }
        public Boolean UserAvailability { get; set; }
        public int FK_RoleID { get; set; }
        public PersonForCreationDto Persons { get; set; }
        public DateTimeOffset EmployeeCreatedate { get; set; } = DateTime.Now;

    }
}
