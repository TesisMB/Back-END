using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SICREYD.Models
{
    public class UsersForCreationDto
    {

        //Todas estas variables van a ser necesarias a la hora de crear un nuevo Usuario

        public string UserFirstName { get; set; }

        public string UserLastname { get; set; }

        public string UserDni { get; set; }

        public string UserPassword { get; set; }

        public string UserPhone { get; set; }

        public string UserEmail { get; set; }

        public string UserGender { get; set; }

        public string UserAddress { get; set; }

        public DateTimeOffset UserBirthdate { get; set; }

        public DateTimeOffset UserCreatedate { get; set; }

        public string UserAvatar { get; set; }

        public int IdRole { get; set; } /*Una vez que el Usuario Ingresa el Id automaticamente 
                                        se le va colocar el nombre del rol al cual pertence ese nuevo usuario */


    }
}
