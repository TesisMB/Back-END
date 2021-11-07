using Back_End.Entities;
using Back_End.Models.Employees___Dto;
using Back_End.Profiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class UsersEmployeesForCreationDto 
    {
        public string UserDni { get; set; }
        public string UserPassword { get; set; }

        public Boolean UserAvailability { get; set; } = true;

        public int FK_RoleID { get; set; }

        public string ResetToken { get; set; } = null;

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public PersonForCreationDto Persons { get; set; }
        public int FK_EstateID { get; set; }

        public int FK_LocationID { get; set; }


    }
}
