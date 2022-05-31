using Back_End.Models;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IEmployeesRepository : IRepositoryBase<Employees>
    {
        Task<IEnumerable<Employees>> GetAllEmployees(int userId);

        Task<Employees> GetEmployeeById(int employeeId);

        Employees GetEmployeeWithDetails(int employeeId);

        void CreateEmployee(Users employee);

        void UpdateEmployee(Employees employee);

    }
}
