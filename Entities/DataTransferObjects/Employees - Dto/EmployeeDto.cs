using Back_End.Models.Users___Dto.Users___Persons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Employees___Dto
{
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }

        public DateTime EmployeeCreatedate { get; set; }
        public EmployeeUserDto Users { get; set; }
    }
}
