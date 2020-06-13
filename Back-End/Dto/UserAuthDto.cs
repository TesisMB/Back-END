using Back_End.Models;
using System.Collections.Generic;

namespace Back_End.Dto
{
    //Esta clase va a devolver determinados datos respecto del Usuario que acaba de iniciar session 
    public class UserAuthDto
    {
        // Este Metodo tiene dos parametros UserName y BearerToken
        public UserAuthDto() : base()
        {
            UserName = "No Autorizado"; //Inicalizo la variable con un valor por default
            BearerToken = string.Empty; // El token queda vacio 
        }

        public string UserName { get; set; } //nombre del Usuario  (Dni)


        public string BearerToken { get; set; } // Token


        public bool IsAuthenticated { get; set; } //Devuelve True o false 


        public List<Permissions> Permissions { get; set; } //Lista todos los permisos que puede realizar el Usuario.
    }

}