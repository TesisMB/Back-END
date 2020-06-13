using SICREYD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SICREYD.Models
{
    //Esta clase va a devolver determinados datos respecto del Usuario que acaba de iniciar session 
    public class UserAuthDto
    {
        // Este Metodo tiene dos parametros DNI y BearerToken
        public UserAuthDto() : base()
    {
        Dni = "No Autorizado"; //Inicializo la variable con un valor por default
            BearerToken = string.Empty; // El token queda vacio 
    }

    public string Dni { get; set; } //nombre del Usuario  (Dni)


    public string BearerToken { get; set; } // Token


    public bool IsAuthenticated { get; set; } //Devuelve True o false 


    public List<Permissions> Permissions { get; set; } //Lista todos los permisos que puede realizar el Usuario.
}

}