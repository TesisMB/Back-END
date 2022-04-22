using Back_End.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;

namespace Repository
{
    public class Email
    {

        public static void generatePassword(Users user)
        {
            int longitud = 8;
            Guid miGuid = Guid.NewGuid();

            //convierto de Guid a byte
            //miGuid.ToByteArray() => Representa ese tipo guid como una matriz de bytes
            string token = Convert.ToBase64String(miGuid.ToByteArray());

            //Replazo los = y el signo +
            token = token.Replace("=", "").Replace("+", "");

            string codigo = token.Substring(0, longitud);

            user.UserPassword = codigo;

        }

        public static void sendVerificationEmail(Users user)
        {
            string message;

            message = $@"<p>Se creo con exito su cuenta</p>
             <p>Su usuario es: {user.UserDni}<p>
             <p>Su contraseña es: {user.UserPassword}</p>";

            Email.Send(
                to: user.Persons.Email,
                subject: "Sign-up Verification API",
                html: $@"<p>Bienvenido a SICREYD!</p>
                         {message}"
            );
        }


        public static void sendResourcesRequest(Users user, Users user2)
        {
            string message;

            //Falta legajo - Dni
            message = $@"<p>Nueva solicitud de recursos</p>
                         <p>El Usuario: {user.Persons.FirstName} {user.Persons.LastName}
                            hizo una nueva solicitud</p>";

            Email.Send(
                to: user2.Persons.Email,
                subject: "Solicitud de recursos",
                html: $@"{message}"
                );
        }

        //Legajo - actualizacion pedido


        //Falta quien lo rechazo y email.
        public static void sendAcceptRejectRequest(Users user, string condition)
        {
            string message;

            message = $@"<p>Estado de solicitud de recursos</p>
                         <p>Su solicitud fue {condition}</p>
                        ";

            Email.Send(
                to: user.Persons.Email,
                subject: "Estado de solicitud de recursos",
                html: $@"{message}"
                );
        }



        public static void Send(string to, string subject, string html, string from = null)
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
            smtp.Authenticate("tesis.unc.mb@gmail.com", "laRioja1450");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

