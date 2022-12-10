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
using System.Globalization;

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

            Email.generatePassword(authUser);
            sendPasswordResetEmail(authUser);

            authUser.UserPassword = Encrypt.GetSHA256(authUser.UserPassword);


            Update(authUser);

            SaveAsync();

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
        public void sendPasswordResetEmail(Users account)
        {
            string message;
            Mail msg;
            {


                    var resetUrl = $"http://localhost:4200/cliente/resetear-contrase%C3%B1a?token={account.ResetToken}";

                    resetUrl.Trim();

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
                                       '>Restablecimiento de la contraseña</h2>
                                    </div>
                                    <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                        <p style='margin-left: 20px;'>Nos hemos enterado de que has perdido tu contraseña de SICREYD. Lo sentimos.
                                        </p>
                                        <p style='margin-left: 20px;'>Pero no te preocupes. Puedes utilizar la siguiente contraseña: </p>

                                        <p
                                            style='margin-left: 20px; text-align: center; margin-top: 1.5rem; font-size: 20px; letter-spacing: 2px; font-weight: normal;'>
                                            {account.UserPassword}
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
                     msg = new Mail(new string[] { account.Persons.Email }, "Restablecer contraseña", $@"{message}");

                EmailSender.SendEmail(msg);

                //Email.Send
                //    (
                //    to: account.Persons.Email,
                //    subject: "Sign-up Verification API - Reset Password",
                //   html: $@"<h4>Reset Password Email</h4>
                //             {message}"
                //);
            }
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
                a => a.Estates.EstateID == user.FK_EstateID
                && a.FK_RoleID != 1
                && a.UserID != userId);

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


        public async Task<IEnumerable<Users>> Get(int userId)
        {

            var user = EmployeesRepository.GetAllEmployeesById(userId);


            var collection = _cruzRojaContext.Users as IQueryable<Users>;


            collection = collection.Where(
                a => a.Estates.EstateID == user.FK_EstateID);

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

        public void sendLoginNotification(Users account)
        {
            string message;
            DateTime localDate = DateTime.Now;
            String cultureName = "es-AR";

           
                var culture = new CultureInfo(cultureName);
                Console.WriteLine("{0}:", culture.NativeName);
                Console.WriteLine("Local date and time: {0}",localDate.ToString(culture));
            
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
                                       '>Aviso de inicio de sesión</h2>
                                    </div>
                                    <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                        <p style='margin-left: 20px;'>Hola, te avisamos que el usuario {account.Persons.LastName} , {account.Persons.FirstName} inicio sesion a las {localDate.ToString(culture)}.
                                        </p>

                                        <p style='margin-top: 2rem; margin-left: 20px;'>
                                            Gracias.
                                        </p>
                                        <p style='margin-left: 20px;'>
                                            El equipo de SICREYD.
                                        </p>
                                </div>
                                        ";
                    string[] teamEmail = { "matias_roldan89@hotmail.com", "yoelsolca5@gmail.com", "maxigalancid@gmail.com" };


                string subject = $@"""{account.Persons.LastName} {account.Persons.FirstName}"" ha iniciado sesion";
                Mail msg = new Mail(teamEmail, subject, $@"{message}");

                EmailSender.SendEmail(msg);

            }
        }

    }



}
