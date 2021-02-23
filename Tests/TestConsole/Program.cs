using System;
using System.Net;
using System.Net.Mail;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var from = new MailAddress("");
            var to = new MailAddress("");

            var message = new MailMessage(from, to);
            message.Subject = "Заголовок";
            message.Body = "Текст письма";

            var client = new SmtpClient("smtp.yandex.ru", 465);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "",
                //SecurePassword = 
                Password = ""
            };

            client.Send(message);
        }
    }
}
