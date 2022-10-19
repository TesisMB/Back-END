using Entities.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    //public partial interface IEmailSender
    //{
    //    static void SendEmail(Mail mail);
    //}



    public partial class EmailSender 
    {
        private static readonly EmailConfiguration _emailConfiguration;

        [Obsolete]
        public static void SendEmail(Mail message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        [Obsolete]
        private static MimeMessage CreateEmailMessage(Mail message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("yoelsolca5@gmail.com"));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format(message.Content) };

            return emailMessage;
        }

        private static void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    //client.Authenticate("tesis.unc.mb@gmail.com", "acfm vbmz zbuo qnpt");
                    client.Authenticate("yoelsolca5@gmail.com", "bhrt tpzl qlmj zffc");

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
