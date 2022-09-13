using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeesRepository : RepositoryBase<Employees>, IEmployeesRepository
    {
        private static CruzRojaContext _cruzRojaContext = new CruzRojaContext();

        public static Users user;


        private IMapper _mapper;

        public EmployeesRepository(CruzRojaContext cruzRojaContext, IMapper mapper) : base(cruzRojaContext)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<Employees>> GetAllEmployees(int userId)
        {
            //var user = UsersRepository.authUser;

          
            var Collection = _cruzRojaContext.Employees as IQueryable<Employees>;

            var locations =  GetAllEmployeesById(userId);


            Collection = Collection.Where(
                a => a.Users.Estates.Locations.LocationDepartmentName == locations.Estates.Locations.LocationDepartmentName
                && a.Users.Estates.Locations.LocationCityName == locations.Estates.Locations.LocationCityName
                && a.Users.Estates.Locations.LocationMunicipalityName == locations.Estates.Locations.LocationMunicipalityName);

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


        public static Users GetAllEmployeesById(int id)
        {
            user =  _cruzRojaContext.Users
                .Where(a => a.UserID.Equals(id))
                .Include(a => a.Persons)
                .Include(a => a.Roles)
                .Include(a => a.Estates)
                .ThenInclude(a => a.Locations)
                .AsNoTracking()
                .FirstOrDefault();

            return user;
        }

        public async Task<Employees> GetEmployeeById(int employeeId)
        {
            return await FindByCondition(empl => empl.EmployeeID.Equals(employeeId))
                    .Include(i => i.Users)
                    .Include(i => i.Users.Persons)
                    .FirstOrDefaultAsync();
        }

        public Employees GetEmployeeWithDetails(int employeeId)
        {
            return FindByCondition(empl => empl.EmployeeID.Equals(employeeId))
                     .Include(i => i.Users)
                    .Include(i => i.Users.Roles).Include(i => i.Users.Persons)
                    .Include(i => i.Users.Estates)
                    .ThenInclude(i => i.LocationAddress)
                    .Include(a => a.Users.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .Include(a => a.Users.Estates.Locations)
                    .FirstOrDefault();
        }

        public void CreateEmployee(Users employee)
        {

            //Email.generatePassword(employee);

            //TO DO Falta hacer funcionar email (Facultad)

            //Email.sendVerificationEmail(employee);
            //spaceCamelCase(employee);

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
