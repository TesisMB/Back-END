using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Employees___Dto
{
    public class EmployeePersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public Boolean Status { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
