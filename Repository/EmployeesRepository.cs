using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeesRepository : RepositoryBase<Employees>, IEmployeesRepository
    {
        private IMapper _mapper;
        //ctor
       
        public EmployeesRepository(CruzRojaContext repositoryContext, IMapper mapper) : base(repositoryContext)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<Employees>> GetAllEmployees()
        {
            return await FindAll()
                    .Include(i => i.Users)
                    .ThenInclude(i => i.Roles)
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
                     .ThenInclude(i => i.Roles)
                    .Include(i => i.Users.Persons)
                    .Include(i => i.Users.Estates)
                    .ThenInclude(i => i.LocationAddress)
                    .Include(a => a.Users.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .Include(a=>a.Users.Estates.Locations)
                    .FirstOrDefaultAsync();
        }

        public void CreateEmployee(EmployeesForCreationDto employee)
        {
            int longitud = 7;
            Guid miGuid = Guid.NewGuid();

            //convierto de Guid a byte
            //miGuid.ToByteArray() => Representa ese tipo guid como una matriz de bytes
            string token = Convert.ToBase64String(miGuid.ToByteArray());

            //Replazo los = y el signo +
            token = token.Replace("=", "").Replace("+", "");

            //Devuelve los caracteres extraídos de una cadena según la posición 
            //del carácter especificado para una cantidad especificada de caracteres.
            string codigo = token.Substring(0, longitud);

            employee.Users.UserPassword = codigo;

            var employeeEntity = _mapper.Map<Employees>(employee);

            employeeEntity.Users.UserPassword = Encrypt.GetSHA256(employee.Users.UserPassword);

            Create(employeeEntity);

            SaveAsync();

            sendVerificationEmail(employee);

            //Create(employee);
        }

        private void sendVerificationEmail(EmployeesForCreationDto employees)
        {
            string message;

             message = $@"<p>Se creo con exito su cuenta</p>
             <p>Su usuario es: {employees.Users.UserDni}<p>
             <p>Su contraseña es: {employees.Users.UserPassword}</p>";

            EmailRepository.Send(
                to: employees.Users.Persons.Email,
                subject: "Sign-up Verification API",
                html: $@"<p>Bienvenido a SICREYD!</p>
                         {message}"
            );
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

        public void UpdateEmployee(Employees employee)
        {
            Update(employee);
        }
    }
}
