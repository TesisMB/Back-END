using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SICREYD.Models
{
    public class UsersForCreationDto
    {

        //Todas estas variables van a ser necesarias a la hora de crear un nuevo Usuario

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Dni { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

      //  public DateTimeOffset DateOfCreate { get; set; }

        public int IdRole { get; set; } /*Una vez que el Usuario Ingresa el Id automaticamente 
                                        se le va colocar el nombre del rol al cual pertence ese nuevo usuario */


    }
}
