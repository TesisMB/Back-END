using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Entities;
namespace Back_End.Models
{

    public class UsersDto:RolesDto
        /*Me va a permitir heredar las variables tanto el RoleID 
                                       como el RoleName al momento de listar los Usuarios*/
    {
        public int ID { get; set; }
        public string UserDni { get; set; }
        public Boolean UserAvailable { get; set; }
        public RolesDto Roles { get; set; }
        public PersonsDto Persons { get; set; }
    }
}
