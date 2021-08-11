using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {

        private IMapper _mapper;
        private CruzRojaContext _cruzRojaContext;

        public UsersRepository(CruzRojaContext cruzRojaContext, IMapper mapper)
            : base(cruzRojaContext)
        {
            _mapper = mapper;
            _cruzRojaContext = cruzRojaContext;
        }

        public async Task<Users> GetUserEmployeeById(int userId)
        {
            return await FindByCondition(emp => emp.UserID.Equals(userId))
                    .Include(a => a.Employees)
                    .Include(a => a.Persons)
                    .FirstOrDefaultAsync();
        }

        public async Task<Users> GetUserVolunteerById(int userId)
        {
            return await FindByCondition(emp => emp.UserID.Equals(userId))
                              .Include(a => a.Volunteers)
                              .Include(a => a.Persons)
                              .Include(a => a.Volunteers.VolunteersSkills)
                              .ThenInclude(a => a.Skills)
                              .FirstOrDefaultAsync();
        }

        public void DeletUser(Users user)
        {
            Delete(user);
        }


        //Funcion que sirve para poder recuperar la contraseña olvidada
        public void ForgotPassword(ForgotPasswordRequest model)
        {
            Users authUser = null;

            using (var db = new CruzRojaContext())
                authUser = db.Users
                               .Include(u => u.Persons)
                               .Where(u => u.Persons.Email == model.Email).FirstOrDefault();

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
               var resetUrl = $"/account/reset-password?token={account.ResetToken}";
               message = $@"
                                <p>Sentimos que hayas tenido problemas para iniciar sesión en SYNAGIR. Podemos ayudar a recuperar tu cuenta.</p>
                                <p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                                 <a style=""color: white; text-align: center; display: block; background:red; text-decoration: none;  border-radius: 0.4rem; margin: 4rem auto; width: 15%; padding: 10px;
                                    "" href=""{resetUrl}"">Reset your Password</a>";



                // color: white; text - align: center; display: block; background: red; width: 20 %; margin: 10rem auto; padding: 10px; border - radius: 0.4rem;
                // < p ><a href=""{resetUrl}"">{resetUrl}</a></p>";
                //message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                //           <p><code>{account.ResetToken}</code></p>";
            }

            EmailRepository.Send
                (
                to: account.Persons.Email,
                subject: "Sign-up Verification API - Reset Password",
               html: $@"<h4>Reset Password Email</h4>
                         {message}"
            );
        }

        //Funcion para validar los campos: Token, contraseña nueva 
        public void  ResetPassword(string token, ResetPasswordRequest model)
        {
           Users account = null;

           using (var db = new CruzRojaContext())
                account = db.Users
                               .Where(u => u.ResetToken == token
                               && u.ResetTokenExpires > DateTime.Now).FirstOrDefault();

            /*var account = _cruzRojaContext.Users.SingleOrDefault
                (i => i.ResetToken == token
                && i.ResetTokenExpires > DateTime.Now);*/

            /*if (account == null)
                throw new ArgumentException("Invalid token");*/

            account.UserPassword = Encrypt.GetSHA256(model.Password);
            account.PasswordReset = DateTime.Now;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            Update(account);
            SaveAsync();
        }

        public async Task<UserEmployeeAuthDto> ValidateUser(UserLoginDto user)
        {
            UserEmployeeAuthDto ret = new UserEmployeeAuthDto();
            Users authUser = null;

            string Pass = user.UserPassword;
            string ePass = Encrypt.GetSHA256(Pass);

            //se conecta a la base de datos para verificar las datos del usuario en cuestion
             await using (var db = new CruzRojaContext())
                authUser = db.Users.Include(u => u.Persons)
                               .Include(u => u.Roles)
                               .Include(u => u.Estates)
                               .ThenInclude(u => u.LocationAddress)
                               .Include(u => u.Estates.EstatesTimes)
                               .ThenInclude(u => u.Times)
                               .ThenInclude(u => u.Schedules)
                                .Where(u => u.UserDni == user.UserDni
                                   && u.UserPassword == ePass).FirstOrDefault();

            if (authUser != null)
            {
                ret = _mapper.Map<UserEmployeeAuthDto>(authUser); //si los datos son correctos se crea el objeto del usuario autentificado
            }

            return ret; //retornamos el valor de este objeto          
        }

        /*public void Send(string to, string subject, string html, string from = null)
            {
                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(from ?? "info@aspnet-core-signup-verification-api.com"));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = html };


                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("tesis.unc.mb@gmail.com", "larioja1450 ");

                smtp.Send(email);
                smtp.Disconnect(true);
            }*/
    }
 }
