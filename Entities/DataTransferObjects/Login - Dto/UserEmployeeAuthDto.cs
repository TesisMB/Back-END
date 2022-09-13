using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.Employees___Dto;
using System;


namespace Back_End.Models
{
    //Esta clase va a devolver determinados datos respecto del Usuario que acaba de iniciar session 
    public class UserEmployeeAuthDto
    {
        public int UserID { get; set; }
        public string UserDni { get; set; }
        public Boolean UserAvailability { get; set; }
        public string RoleName { get; set; }

        public EmployeePersonDto Persons { get; set; }
        public EstatesDto Estates { get; set; }

        public string Avatar { get; set; }
        public string CreatedDate { get; set; }
        public string token { get; set; }

        //El token surgue de realizar el llamada a la funcion GenerateAccessToken usando automapper
    }
}