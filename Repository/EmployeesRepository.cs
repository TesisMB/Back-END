using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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


        public static Users GetAllEmployeesById(int? id)
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

            Email.generatePassword(employee);

            sendVerificationEmail(employee);

            spaceCamelCase(employee);

            employee.UserPassword = Encrypt.GetSHA256(employee.UserPassword);


            UsersRepository.CreateUser(employee);
        }



        public static void sendVerificationEmail(Users user)
        {
            string message;

            if(user.FK_RoleID != 5)
            {
            message = $@"
                                <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>

                                    <h2 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal;
                                    position: relative;
                                    margin-bottom: 1rem;
                                   '>¡Bienvenido!</h2>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style='margin-left: 20px;'>
                                    Estimado {user.Persons.FirstName},
                                    </p>

                                    <p style='margin-left: 20px;'>
                                        Te damos la bienvenida a tu cuenta. Los datos para acceder son
                                        los siguientes:
                                    </p>
                                    <p style='margin-left: 20px;  margin-top: 1.5rem;  font-weight: normal;'>
                                        Documento: {user.UserDni}
                                    </p>
                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        Contraseña: {user.UserPassword}
                                    </p>
                                    <a style='color: white;
                                    text-align: center;
                                display: block;
                                    background: rgb(189, 45, 45);
                                text-decoration: none;
                                    border-radius: 0.4rem;
                                     width: 33%;
                                     margin-top: 2rem;
                                    margin-bottom: 2rem;
                                    padding: 15px; cursor: pointer; margin-left: 10rem;' href='https://calm-dune-0fef6d210.2.azurestaticapps.net/'>Ir a SICREYD</a>

                                    <p style='margin-left: 20px; margin-top: 30px;'>
                                        Recuerda que puedes cambiar tu contraseña todo el tiempo desde tu perfil.
                                    </p>

                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Gracias.
                                    </p>
                                    <p style='margin-left: 20px;'>
                                        El equipo de SICREYD.
                                    </p>
                            </div>

             ";
            }
            else
            {
                message = $@"
                                <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>

                                    <h2 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal;
                                    position: relative;
                                    margin-bottom: 1rem;
                                   '>¡Bienvenido!</h2>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style='margin-left: 20px;'>
                                    Estimado {user.Persons.FirstName},
                                    </p>

                                    <p style='margin-left: 20px;'>
                                        Te damos la bienvenida a tu cuenta. Los datos para acceder son
                                        los siguientes:
                                    </p>
                                    <p style='margin-left: 20px;  margin-top: 1.5rem;  font-weight: normal;'>
                                        Documento: {user.UserDni}
                                    </p>
                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        Contraseña: {user.UserPassword}
                                    </p>
                                    <p style='margin-left: 20px;'>
                                        Recuerda que puedes cambiar tu contraseña todo el tiempo desde tu perfil.
                                    </p>

                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Gracias.
                                    </p>
                                    <p style='margin-left: 20px;'>
                                        El equipo de SICREYD.
                                    </p>
                            </div>

             ";
            }


            var msg = new Mail(new string[] { user.Persons.Email }, "Registro exitoso", $@"{message}");

            EmailSender.SendEmail(msg);

            //Email.Send(
            //    to: user.Persons.Email,
            //    subject: "Sign-up Verification API",
            //    html: $@"<p>Bienvenido a SICREYD!</p>
            //             {message}"
            //);
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
