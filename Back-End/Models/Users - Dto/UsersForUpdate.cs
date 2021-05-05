using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    //Esta clase que extiende de Users define aquellos valores autorizados que pueden ser actualizados.
    public class UsersForUpdate
    {
        public string UserPassword { get; set; }
        public Boolean UserAvailability { get; set; }
        public int FK_RoleID { get; set; }
        public PersonsForUpdatoDto Persons { get; set; }
    }
}
