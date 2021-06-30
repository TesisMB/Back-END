using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class EmployeesRepository : RepositoryBase<Employees>, IEmployeesRepository
    {
        //ctor
        public EmployeesRepository(CruzRojaContext cruzRojaContext2)
            : base(cruzRojaContext2)
        {

        }
        public IEnumerable<Employees> GetAllEmployees()
        {
            return FindAll()
                    .Include(i => i.Users)
                    .ThenInclude(i => i.Roles)
                    .Include(i => i.Users.Persons)
                    .Include(i => i.Users.Estates)
                    .ThenInclude(i => i.LocationAddress)
                    .Include(a => a.Users.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .ToList();
        }

        public Employees GetEmployeeById(int employeeId)
        {
            return FindByCondition(empl => empl.EmployeeID.Equals(employeeId))
                    .Include(i => i.Users)
                    //    .ThenInclude(i => i.Roles)
                    .Include(i => i.Users.Persons)
                    .Include(i => i.Users.Estates)
                    .ThenInclude(i => i.LocationAddress)
                    .Include(a => a.Users.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .FirstOrDefault();
        }

        public Employees GetEmployeeWithDetails(int employeeId)
        {
            return FindByCondition(empl => empl.EmployeeID.Equals(employeeId))
                    .Include(a => a.Users)
                    //.Include(a => a.Vehicles)
                    .FirstOrDefault();
        }
        public void CreateEmployee(Employees employee)
        {
            Create(employee);
        }

        public void UpdateEmployee(Employees employee)
        {
            Update(employee);
        }
    }
}
