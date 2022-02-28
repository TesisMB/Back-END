using Entities.DataTransferObjects.Estates___Dto;
using System;


namespace Back_End.Models.Users___Dto.Users___Persons
{
    public class EmployeesUsersDto
    {
        public int UserID { get; set; }
        public Boolean UserAvailability { get; set; }

        public string UserDni { get; set; }
        public string RoleName { get; set; }

        public string Name { get; set; }
 
        public Boolean Status { get; set; }


    }
}
