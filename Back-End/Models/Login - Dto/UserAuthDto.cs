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
    public string token { get; set; } 
    public bool IsAuthenticated { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public DateTimeOffset Birthdate { get; set; }
    public Boolean Available { get; set; }
    public string RoleName { get; set; }
    }
}