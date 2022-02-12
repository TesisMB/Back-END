using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Helpers;
using Microsoft.EntityFrameworkCore;
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
                    .Include(i => i.Users.Locations)
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
                     .Include(i => i.Users.Locations)
                    .Include(i => i.Users.Roles).Include(i => i.Users.Persons)
                    .Include(i => i.Users.Estates)
                    .ThenInclude(i => i.LocationAddress)
                    .Include(a => a.Users.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .Include(a => a.Users.Estates.Locations)
                    .FirstOrDefaultAsync();
        }

        public void CreateEmployee(Employees employee)
        {
            //int longitud = 7;
            //Guid miGuid = Guid.NewGuid();

            //convierto de Guid a byte
            //miGuid.ToByteArray() => Representa ese tipo guid como una matriz de bytes
            // string token = Convert.ToBase64String(miGuid.ToByteArray());

            //Replazo los = y el signo +
            //token = token.Replace("=", "").Replace("+", "");

            //Devuelve los caracteres extraídos de una cadena según la posición 
            //del carácter especificado para una cantidad especificada de caracteres.
            //string codigo = token.Substring(0, longitud);

            //employee.Users.UserPassword = codigo;


            Email.generatePassword(employee.Users);

            Email.sendVerificationEmail(employee.Users);
            spaceCamelCase(employee);

            employee.Users.UserPassword = Encrypt.GetSHA256(employee.Users.UserPassword);

            Create(employee);

            //            SaveAsync();


        }


        public static Employees spaceCamelCase(Employees employee)
        {
            employee.Users.UserDni = WithoutSpace_CamelCase.GetCamelCase(employee.Users.UserDni);
            employee.Users.UserPassword = WithoutSpace_CamelCase.GetWithoutSpace(employee.Users.UserPassword);
            employee.Users.Persons.FirstName = WithoutSpace_CamelCase.GetCamelCase(employee.Users.Persons.FirstName);
            employee.Users.Persons.LastName = WithoutSpace_CamelCase.GetCamelCase(employee.Users.Persons.LastName);
            employee.Users.Persons.Phone = WithoutSpace_CamelCase.GetCamelCase(employee.Users.Persons.Phone);
            employee.Users.Persons.Address = WithoutSpace_CamelCase.GetCamelCase(employee.Users.Persons.Address);
            employee.Users.Persons.Email = WithoutSpace_CamelCase.GetWithoutSpace(employee.Users.Persons.Email);

            return employee;
        }

        private void sendVerificationEmail(EmployeesForCreationDto employees)
        {
            string message;

            message = $@"<p>Se creo con exito su cuenta</p>
             <p>Su usuario es: {employees.Users.UserDni}<p>
             <p>Su contraseña es: {employees.Users.UserPassword}</p>";

            Email.Send(
                to: employees.Users.Persons.Email,
                subject: "Sign-up Verification API",
                html: $@"<p>Bienvenido a SICREYD!</p>
                         {message}"
            );
        }

        public void UpdateEmployee(Employees employee)
        {
            Update(employee);
        }
    }
}
