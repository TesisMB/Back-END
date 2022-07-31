using System;

namespace Entities.DataTransferObjects.Employees___Dto
{
    public class EmployeeDto
    {

        public int EmployeeID { get; set; }
        public DateTime EmployeeCreatedate { get; set; }

        public EmployeeUserDto Users { get; set; }
    }
}
