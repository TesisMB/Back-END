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
        // Este Metodo tiene dos parametros DNI y BearerToken
        public UserAuthDto() : base()
    {
        UserDni = "No Autorizado"; //Inicializo la variable con un valor por default
        token = string.Empty; // El token queda vacio 
    }
        
    public int ID { get; set; }
    public string UserDni { get; set; } 
    public string RoleName { get; set; }
    public string token { get; set; } 
    public Boolean UserAvailable { get; set; } //Esto es necesario ya que se usa en el Login para devolver un 200 o 401
   
    public Boolean Status { get; set; }

    }
}