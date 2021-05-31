using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class EmployeesForCreationDto
    {
        public DateTimeOffset EmployeeCreatedate { get; set; } = DateTime.Now;

    }
}
