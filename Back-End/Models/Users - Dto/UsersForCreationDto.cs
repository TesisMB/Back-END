using Back_End.Entities;
using Back_End.Models.Users_Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class UsersForCreationDto  : UserDniMustBeDifferentFromUserDniDto
    {
        [UserDniMustBeDifferentFromUserDniDto(
       ErrorMessage = "El Dni que ingreso ya existe")]
        public string UserDni { get; set; }
        public string UserPassword { get; set; }
        public Boolean UserAvailability { get; set; }
        public int FK_RoleID { get; set; }
        public PersonForCreationDto Persons { get; set; }
        public DateTimeOffset EmployeeCreatedate { get; set; } = DateTime.Now;

    }
}
