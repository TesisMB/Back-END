using Back_End.Models.Employees___Dto;
using Back_End.Models.Users___Dto.Users___Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class EmployeesDto
    {
        public int EmployeeID { get; set; }
        public DateTime EmployeeCreatedate { get; set; }
        public EmployeesUsersDto Users { get; set; }
    }
}
