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

        //public static void sendVerificationEmail(Users user)
        //{
        //    string message;


        //     message = $@"
        //      <div style='display: flex;
        //                                  justify-content: center;
        //                                  align-items: center;
        //                                  height: 96vh;'>

        //            <div style='border: 1px solid #ccc;
        //                        max-width: 58%;
        //                        border-radius: 6px;
        //                        display: flex;
        //                        align-items: center;
        //                        justify-content: center;
        //                        flex-direction: column;
        //                        padding: 20px;'>

        //       <h2 style='font-weight: 400;
        //                  line-height: 30px;
        //                  font-size: 15px;
        //                  margin: 0 0 1rem 0;'>
        //        ¡Bienvenido a SINAGIR!</h2>


        //       <h3 stlye='font-weight: normal;
        //            font-size: 13px;'>
        //        Tu cuenta se creo exitosamente</h3>


        //       <p   stlye='font-weight: normal;
        //            font-size: 13px;'>
        //        Te dejamos los datos para que puedas acceder a tu cuenta</p>

        //                    <div class='datos' style='display: block;
        //                                              width: 100%;'>

        //                        <p stlye='font-weight: normal;
        //                           font-size: 13px;
        //                           text-align: start;
        //                           padding: 1rem 0 0 0;'>Usuario:  {user.UserDni }</p>

        //                        <p stlye='font-weight: normal;
        //                           font-size: 13px;
        //                           text-align: start;
        //                           padding: 1rem 0 0 0;'>Contraseña: {user.UserPassword }</p>
        //                     </div>
        //                </div>
        //            </div>
        //     </div>";



        //    Email.Send(
        //        to: user.Persons.Email,
        //        subject: "Sign-up Verification API",
        //        html: $@"<p>Bienvenido a SICREYD!</p>
        //                 {message}"
        //    );
        //}


        public static void sendResourcesRequest(Users user, Users user2)
        {
            string message;

            // TODO Falta legajo - Dni
            message = $@"<p>Nueva solicitud de recursos</p>
                         <p>El Usuario: {user.Persons.FirstName} {user.Persons.LastName}
                            hizo una nueva solicitud</p>";

            Email.Send(
                to: user2.Persons.Email,
                subject: "Solicitud de recursos",
                html: $@"{message}"
                );
        }

        //TODO Legajo - actualizacion pedido


        //TODO Falta quien lo rechazo y email.
        //public static void sendAcceptRejectRequest(Users user, string condition)
        //{
        //    string message;

        //    message = $@"<p>Estado de solicitud de recursos</p>
        //                 <p>Su solicitud fue {condition}</p>
        //                ";



        //    Email.Send(
        //        to: user.Persons.Email,
        //        subject: "Estado de solicitud de recursos",
        //        html: $@"{message}"
        //        );
        //}



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
            smtp.Connect("smtp.sendgrid.net", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(
                userName: "apikey", 
                password: "JrGnw5aOTYOMysaEbO8mtQ");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

