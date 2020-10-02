using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    //Esta clase va a usarse para que el usuario pueda logearse con su DNI - Contraseña
    public  class UserLoginDto
    {
        public string UserDni { get; set; } //nombre del Usuario  (Dni)
        public string UserPassword { get; set; }
    }
}
