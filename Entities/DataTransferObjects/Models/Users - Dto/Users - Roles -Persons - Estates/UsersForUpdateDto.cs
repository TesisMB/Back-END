using Back_End.Helpers;
using Back_End.Models.Employees___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    //Esta clase que extiende de Users define aquellos valores autorizados que pueden ser actualizados.
    public class UsersForUpdateDto
    {
        public string UserPassword { get; set; }
        public string UserNewPassword { get; set; }
        public Boolean UserAvailability { get; set; }
        public int FK_RoleID { get; set; }
        public int FK_EstateID { get; set; }
        public PersonsForUpdatoDto Persons { get; set; }
    }
}
