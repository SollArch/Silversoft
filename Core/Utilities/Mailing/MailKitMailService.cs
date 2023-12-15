using Core.Mailing;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Core.Utilities.Mailing
{
    public class MailKitMailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailKitMailService(IConfiguration configuration)
        {
            _mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
        }

        public void Send(Mail mail,string password)
        {
            MimeMessage email = new MimeMessage();

            email.From.Add(new MailboxAddress(_mailSettings.SenderFullName, _mailSettings.SenderEmail));

            email.To.Add(new MailboxAddress(mail.ToFullName, mail.ToEmail));

            email.Subject = mail.Subject;

            var bodyBuilder = new BodyBuilder()
            {
                TextBody = mail.TextBody,
            };
            email.Body = bodyBuilder.ToMessageBody();

            SmtpClient smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Server, _mailSettings.Port);
            smtp.Authenticate(_mailSettings.UserName, password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}