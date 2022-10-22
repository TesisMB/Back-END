using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Repository
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        private IMapper _mapper;
        public static Users authUser;
        public static CruzRojaContext _cruzRojaContext;

        public UsersRepository(CruzRojaContext cruzRojaContext, IMapper mapper)
            : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
            _mapper = mapper;
        }

        public async Task<Users> GetUserEmployeeById(int userId)
        {
            return await FindByCondition(emp => emp.UserID.Equals(userId))
                    .Include(a => a.Persons)
                    .Include(a => a.Messages)
                    .Include(a => a.UsersChatRooms)
                    .Include(a => a.Employees.Vehicles)
                    .Include(a => a.Volunteers)
                   .Include(a => a.Volunteers.VolunteersSkills)
                   .ThenInclude(a => a.Skills)
                   .Include(a => a.Volunteers.VolunteersSkills)
                   .ThenInclude(a => a.VolunteersSkillsFormationEstates)
                    .FirstOrDefaultAsync();
        }

        public async Task<Users> SendDeviceById(int userId)
        {
            return await FindByCondition(emp => emp.UserID.Equals(userId))
                    .FirstOrDefaultAsync();
        }

        public async Task<Users> GetUserVolunteerById(int userId)
        {
            return await FindByCondition(emp => emp.UserID.Equals(userId))
                              .Include(a => a.Volunteers)
                              .Include(a => a.Persons)
                              .Include(a => a.Volunteers.VolunteersSkills)
                              .ThenInclude(a => a.Skills)
                              .Include(a => a.Volunteers.VolunteersSkills)
                              .ThenInclude(a => a.VolunteersSkillsFormationEstates)
                              .FirstOrDefaultAsync();
        }

        public void DeletUser(Users user)
        {
            Delete(user);
        }



        public static void CreateUser(Users user)
        {
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            cruzRojaContext.Add(user);

            cruzRojaContext.SaveChanges();
        }


        //Funcion que sirve para poder recuperar la contraseña olvidada
        public void ForgotPassword(string email)
        {
            Users authUser = null;

            using (var db = new CruzRojaContext())
                authUser = db.Users
                               .Include(u => u.Persons)
                               .Where(u => u.Persons.Email == email)
                               .AsNoTracking()
                               .FirstOrDefault();

            //var account = _cruzRojaContext.Users.FirstOrDefault(i => i.Persons.Email == model.Email);

            if (authUser == null) return;

            authUser.ResetToken = randomTokenString();
            authUser.ResetTokenExpires = DateTime.Now.AddDays(1);

            Update(authUser);

            SaveAsync();

            sendPasswordResetEmail(authUser);
        }

        //Funcion para crear un token de manera random
        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }


        //Funcion que permite enviar un email al usuario que se olvido la contraseña.
        private void sendPasswordResetEmail(Users account)
        {
            string message;
            {

                var resetUrl = $"http://localhost:4200/cliente/resetear-contrase%C3%B1a?token={account.ResetToken}";

                resetUrl.Trim();
                message = $@"
                                  <div style='height: 100vh;
                                                border-width: 5px;'>
                                        <div style='margin-top: 1.7rem; text-align: center;
                                                                     margin-right: 15rem'>
                                                        <img src = 'https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png'
                                                        style='height: 37%;
                                                        margin-left: 27%;
                                                        width: 30px;
                                                        border-radius: 50%;
                                                        padding: 8px;
                                                        border: 1px solid #000;'>

                                                        <h1 style='font-size: 24px;
                                                        font-weight: normal;
                                                        font-size: 24px;
                                                        font-weight: normal; margin: 0; margin-left: 16rem;
                                                        margin-top: 5px;'>SICREYD</h1>

                                                        <h2 style='font-size: 24px;
                                                        font-weight: normal;
                                                        font-size: 24px;
                                                        font-weight: normal;
                                                        position: relative;
                                                        margin-left: 27%;
                                                        right: 65px;'>Restablecimiento de la contraseña</h2>
                                        </div>
                                        <div style=' width: 512px;
                                                    padding: 25px;
                                                    border-radius: 8px;
                                                    border: 1px solid #ccc;
                                                    margin-left: 20rem;'>
                                            <p style='margin-left: 20px;'>Nos hemos enterado de que has perdido tu contraseña de SICREYD.Lo sentimos.
                                            </p>
                                            <p style='margin-left: 20px;'>Pero no te preocupes. Puedes utilizar el siguiente botón para restablecer tu contraseña: </p>

                                            <a style = 'color: white;
                                            text-align: center;
                                        display: block;
                                            background: rgb(189, 45, 45);
                                        text-decoration: none;
                                            border-radius: 0.4rem;
                                             width: 33%;
                                             margin-top: 2rem;
                                            margin-bottom: 2rem;
                                            padding: 15px; cursor: pointer; margin-left: 10rem;'  href='{resetUrl}'>Restablecer la contraseña</a>

                                               <p style='margin-left: 20px;'>
                                                    Si no utiliza este enlace en un plazo de 1 dia, caducará.Para obtener un nuevo enlace para
                                                    restablecer la contraseña, visite: 
                                                    <span>
                                                        <a
                                                            href = 'https://calm-dune-0fef6d210.2.azurestaticapps.net/' > https://calm-dune-0fef6d210.2.azurestaticapps.net/
                                                        </a>
                                                    </span>
                                                </p>
                                                <p style = 'margin-top: 2rem; margin-left: 20px;'>
                                                    Gracias,
                                                </p>
                                            <p style='margin-left: 20px;'>
                                                El equipo de SICREYD
                                            </p>
                                        </div>
                                    </div>
                                        ";
                 }

            var msg = new Mail(new string[] { account.Persons.Email }, "Restablecer contraseña", $@"{message}");

            EmailSender.SendEmail(msg);

            //Email.Send
            //    (
            //    to: account.Persons.Email,
            //    subject: "Sign-up Verification API - Reset Password",
            //   html: $@"<h4>Reset Password Email</h4>
            //             {message}"
            //);
        }

        //Funcion para validar los campos: Token, contraseña nueva 
        public void ResetPassword(string token, string password)
        {
            Users account = null;

            using (var db = new CruzRojaContext())
                account = db.Users
                               .Where(u => u.ResetToken == token.Trim()
                               && u.ResetTokenExpires > DateTime.Now)
                               .AsNoTracking()
                               .FirstOrDefault();

            account.UserPassword = Encrypt.GetSHA256(password);
            account.PasswordReset = DateTime.Now;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            Update(account);

            SaveAsync();
        }

        public async Task<UserEmployeeAuthDto> ValidateUser(UserLoginDto user)
        {
            UserEmployeeAuthDto ret = new UserEmployeeAuthDto();

            string Pass = user.UserPassword;
            string ePass = Encrypt.GetSHA256(Pass);

            //se conecta a la base de datos para verificar las datos del usuario en cuestion
            await using (var db = new CruzRojaContext())
                authUser = db.Users
                                .Include(u => u.Persons)
                               .Include(u => u.Roles)
                               .Include(u => u.Estates)
                               .ThenInclude(u => u.LocationAddress)
                               .Include(u => u.Estates.EstatesTimes)
                               .ThenInclude(u => u.Times)
                               .ThenInclude(u => u.Schedules)
                               .Include(u => u.Estates.Locations)
                               .Include(u => u.Volunteers)
                               .Include(u => u.Employees)
                                .Where(u => u.UserDni == user.UserDni
                                   && u.UserPassword == ePass)
                                .AsNoTracking()
                                .FirstOrDefault();


            if (authUser != null)
            {
                ret = _mapper.Map<UserEmployeeAuthDto>(authUser); //si los datos son correctos se crea el objeto del usuario autentificado
            }

            return ret; //retornamos el valor de este objeto       
        }

        public async Task<Users> GetUsers(int userId)
        {
            return await FindByCondition(emp => emp.UserID.Equals(userId))
                                .Include(u => u.Persons)
                               .Include(u => u.Roles)
                               .Include(u => u.Estates)
                               .ThenInclude(u => u.LocationAddress)
                               .Include(u => u.Estates.EstatesTimes)
                               .ThenInclude(u => u.Times)
                               .ThenInclude(u => u.Schedules)
                               .Include(u => u.Estates.Locations)
                               .Include(u => u.Volunteers)
                               .Include(u => u.Employees)
                               .FirstOrDefaultAsync();
          }


        public async Task<IEnumerable<Users>> GetEmployeesVolunteers(int userId)
        {

            var user = EmployeesRepository.GetAllEmployeesById(userId);


            var collection = _cruzRojaContext.Users as IQueryable<Users>;


            collection = collection.Where(
                a => a.Estates.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);

            return await collection
                               .Include(u => u.Persons)
                               .Include(u => u.Roles)
                               .Include(u => u.Estates)
                               .ThenInclude(u => u.LocationAddress)
                               .Include(u => u.Estates.EstatesTimes)
                               .ThenInclude(u => u.Times)
                               .ThenInclude(u => u.Schedules)
                               .Include(u => u.Estates.Locations)
                               .Include(u => u.Volunteers)
                               .Include(u => u.Employees)
                               .OrderBy(a => a.Roles.RoleID)
                               .ToListAsync();
        }

        public async Task<Users> GetEmployeeVolunteerById(int userId)
        {
            return await FindByCondition(emp => emp.UserID.Equals(userId))
                           .Include(u => u.Persons)
                           .Include(u => u.Roles)
                           .Include(u => u.Estates)
                           .ThenInclude(u => u.EstatesTimes)
                           .ThenInclude(u => u.Times)
                           .ThenInclude(u => u.Schedules)

                           .Include(u => u.Estates)
                           .ThenInclude(u => u.LocationAddress)
                           .FirstOrDefaultAsync();
        }   

       
    }
}
