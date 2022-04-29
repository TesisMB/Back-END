using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeesRepository : RepositoryBase<Employees>, IEmployeesRepository
    {
        private CruzRojaContext _cruzRojaContext;

        private IMapper _mapper;
        //ctor

        public EmployeesRepository(CruzRojaContext cruzRojaContext, IMapper mapper) : base(cruzRojaContext)
        {
            _mapper = mapper;
            _cruzRojaContext = cruzRojaContext;
        }

        public async Task<IEnumerable<Employees>> GetAllEmployees()
        {
            var user = UsersRepository.authUser;

            var Collection = _cruzRojaContext.Employees as IQueryable<Employees>;


            Collection = Collection.Where(
                a => a.Users.Estates.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName
                && a.Users.Estates.Locations.LocationCityName == user.Estates.Locations.LocationCityName
                && a.Users.Estates.Locations.LocationMunicipalityName == user.Estates.Locations.LocationMunicipalityName);

            return await Collection
                    .Include(i => i.Users)
                    .Include(i => i.Users.Roles)
                    .Include(i => i.Users.Persons)
                    .Include(i => i.Users.Estates)
                    .ThenInclude(i => i.LocationAddress)
                    .Include(a => a.Users.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .Include(a => a.Users.Estates.Locations)
                    .ToListAsync();
        }

        public async Task<Employees> GetEmployeeById(int employeeId)
        {
            return await FindByCondition(empl => empl.EmployeeID.Equals(employeeId))
                    .Include(i => i.Users)
                    .Include(i => i.Users.Persons)
                    .FirstOrDefaultAsync();
        }

        public async Task<Employees> GetEmployeeWithDetails(int employeeId)
        {
            return await FindByCondition(empl => empl.EmployeeID.Equals(employeeId))
                     .Include(i => i.Users)
                    .Include(i => i.Users.Roles).Include(i => i.Users.Persons)
                    .Include(i => i.Users.Estates)
                    .ThenInclude(i => i.LocationAddress)
                    .Include(a => a.Users.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .Include(a => a.Users.Estates.Locations)
                    .FirstOrDefaultAsync();
        }

        public void CreateEmployee(Users employee)
        {

            Email.generatePassword(employee);

            Email.sendVerificationEmail(employee);
            spaceCamelCase(employee);

            employee.UserPassword = Encrypt.GetSHA256(employee.UserPassword);


            UsersRepository.CreateUser(employee);
        }


        public static Users spaceCamelCase(Users employee)
        {
            employee.UserDni = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(employee.UserDni);
            employee.UserPassword = WithoutSpace_CamelCase.GetWithoutSpace(employee.UserPassword);
            employee.Persons.FirstName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(employee.Persons.FirstName);
            employee.Persons.LastName = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(employee.Persons.LastName);
            employee.Persons.Phone = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(employee.Persons.Phone);
            employee.Persons.Address = WithoutSpace_CamelCase.GetWithoutSpaceWithTitleCase(employee.Persons.Address);
            employee.Persons.Email = WithoutSpace_CamelCase.GetWithoutSpace(employee.Persons.Email);

            return employee;
        }

       

        public void UpdateEmployee(Employees employee)
        {
            Update(employee);
        }
    }
}
