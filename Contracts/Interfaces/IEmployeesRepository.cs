using Back_End.Models;
using System.Collections.Generic;

namespace Contracts.Interfaces
{
    public interface IEmployeesRepository : IRepositoryBase<Employees>
    {
        IEnumerable<Employees> GetAllEmployees();
        Employees GetEmployeeById(int employeeId);

        Employees GetEmployeeWithDetails(int employeeId);

        void CreateEmployee(Employees employee);

        void UpdateEmployee(Employees employee);

    }
}
