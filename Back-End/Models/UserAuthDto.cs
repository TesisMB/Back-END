using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    //Esta clase va a devolver determinados datos respecto del Usuario que acaba de iniciar session 
    public class UserAuthDto
    {
        // Este Metodo tiene dos parametros DNI y BearerToken
        public UserAuthDto() : base()
    {
        UserDni = "No Autorizado"; //Inicializo la variable con un valor por default
            token = string.Empty; // El token queda vacio 
    }

    public string UserDni { get; set; } //nombre del Usuario  (Dni)
    
    public int UserID { get; set; } // ID del usuario

    public string UserFistname { get; set; }

    public string UserLastname { get; set; }

    public string token { get; set; } // Token


    public bool IsAuthenticated { get; set; } //Devuelve True o false 


    public List<Permissions> Permissions { get; set; } //Lista todos los permisos que puede realizar el Usuario.
}

}