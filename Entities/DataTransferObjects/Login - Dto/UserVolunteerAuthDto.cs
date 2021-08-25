using Back_End.Models.Employees___Dto;
using Back_End.Models.Users___Dto.Users___Persons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Login___Dto
{
    public class UserVolunteerAuthDto
    {
        public int UserID { get; set; }
        public string UserDni { get; set; }
        public Boolean UserAvailability { get; set; }
        public string RoleName { get; set; }
        public VolunteersUserDto Volunteers { get; set; }
        public EstatesDto Estates { get; set; }

        //El token surgue de realizar el llamada a la funcion GenerateAccessToken usando automapper
        public string token { get; set; }
    }
}
