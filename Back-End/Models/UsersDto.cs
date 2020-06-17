using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Entities;
namespace Back_End.Models
{

    public class UsersDto : RolesDto /*Me va a permitir heredar las variables tanto el RoleID 
                                       como el RoleName al momento de listar los Usuarios*/
    {
        public int UserID { get; set; }

        public string UserDni { get; set; }
        public string FullName { get; set; } /*Almaceno el Nombre y el Apellido de cada usuario y 
                                             devuelvo el nombre completo  concatenando ambos valores (Name+LastName)*/
       public DateTimeOffset UserBirthdate { get; set; }
        public string UserPhone { get; set; }

        public string UserEmail { get; set; }
        public string UserAddress{ get; set; }
        public string UserAvatar { get; set; }

        public int Age { get; set; } /*Almaceno en la base de datos las fechas de nacimientos
                                      de los usarios y devuelvo la edad correspondientes para cada uno de ellos */

    }
}
