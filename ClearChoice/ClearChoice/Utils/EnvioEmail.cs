using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ClearChoice.Utils
{
    public class EnvioEmail
    {
       public static void Enviar(string email, string senha)
        {
            //Email para:
            MailAddress to = new MailAddress(email);

            //Email de:
            MailAddress from = new MailAddress("personalchoice90@gmail.com");

            MailMessage mail = new MailMessage(from, to);
            
            mail.Subject = "Personal";

            mail.Body = senha;

            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;

                smtp.Credentials = new NetworkCredential(
                    "personalchoice90@gmail.com", "7565295091991");
                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}