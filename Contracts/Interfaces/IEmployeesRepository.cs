using Back_End.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IEmployeesRepository : IRepositoryBase<Employees>
    {
        Task<IEnumerable<Employees>> GetAllEmployees();

        Task<Employees> GetEmployeeById(int employeeId);

        Task<Employees> GetEmployeeWithDetails(int employeeId);

        void CreateEmployee(EmployeesForCreationDto employee);

        void UpdateEmployee(Employees employee);

    }
}
