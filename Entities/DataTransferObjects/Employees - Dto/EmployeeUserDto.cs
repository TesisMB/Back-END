using Back_End.Models.Employees___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Employees___Dto
{
    public class EmployeeUserDto
    {
        public int UserID { get; set; }
        public Boolean UserAvailability { get; set; }

        public string UserDni { get; set; }

        public string RoleName { get; set; }
        public EmployeePersonDto Persons { get; set; }

        public EstatesDto Estates { get; set; }
    }
}
