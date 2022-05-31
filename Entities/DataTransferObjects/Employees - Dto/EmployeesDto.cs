using System;

namespace Back_End.Models
{
    public class EmployeesDto
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public string UserDni { get; set; }

        public string RoleName { get; set; }

        public Boolean UserAvailability { get; set; }

        public Boolean Status { get; set; }

    }
}
