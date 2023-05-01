using BusinessLayer.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace BusinessLayer.Services.GeneralServices
{
    public static class EmailSender
    {
        public static void SendEmailWithCode(string email, EmailMessage emailMessage)
        {
            var Host = Configration.config["Smtp:Host"];
            var UserName = Configration.config["Smtp:UserName"];
            var FromEmail = Configration.config["Smtp:FromEmail"];
            var Password = Configration.config["Smtp:Password"];
            var Port = Convert.ToInt16(Configration.config["Smtp:Port"]);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(UserName, FromEmail));
            message.To.Add(new MailboxAddress("user", email));
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart("plain")
            {
                Text = emailMessage.Body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(Host, Port, false);
                client.Authenticate(FromEmail, Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}
