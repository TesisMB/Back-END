using Back_End.Models.Employees___Dto;
using System;


namespace Back_End.Models.Users___Dto.Users___Persons
{
    public class EmployeesUsersDto
    {
        public int UserID { get; set; }

        public string UserDni { get; set; }

        public string RoleName { get; set; }
        public EmployeesPersonsDto Persons { get; set; }

        public EstatesDto Estates { get; set; }
    }
}
