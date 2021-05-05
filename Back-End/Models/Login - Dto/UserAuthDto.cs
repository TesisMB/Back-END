using Back_End.Entities;
using Back_End.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    //Esta clase va a devolver determinados datos respecto del Usuario que acaba de iniciar session 
    public class UserAuthDto
    {
        public int ID { get; set; }
        public string UserDni { get; set; }
        public Boolean UserAvailability { get; set; }
        public RolesDto Roles { get; set; }
        public PersonsDto Persons { get; set; }

        //El token surgue de realizar el llamada a la funcion GenerateAccessToken usando automapper
        public string token { get; set; } 
    }
}