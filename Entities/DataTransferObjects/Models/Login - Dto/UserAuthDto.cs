using Back_End.Models.Employees___Dto;
using Back_End.Models.Users___Dto.Users___Persons;
using System;


namespace Back_End.Models
{
    //Esta clase va a devolver determinados datos respecto del Usuario que acaba de iniciar session 
    public class UserAuthDto
    {
        public int UserID { get; set; }
        public string UserDni { get; set; }
        public Boolean UserAvailability { get; set; }
        public string RoleName { get; set; }
        public Users_PersonsDto Persons { get; set; }
        public EstatesDto Estates { get; set; }

        //El token surgue de realizar el llamada a la funcion GenerateAccessToken usando automapper
        public string token { get; set; } 
    }
}