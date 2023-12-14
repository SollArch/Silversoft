using System.Net;
using System.Net.Mail;
using Core.Mailing;
using Microsoft.Extensions.Configuration;
namespace Business.Concrete
{
    public class MailManager : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly MailSettings _mailSettings;

        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
        }

        public void Send(Mail mail)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Port = _mailSettings.Port;
            smtpClient.Host = _mailSettings.Server;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_mailSettings.SenderEmail, _mailSettings.Password);
            
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_mailSettings.SenderEmail, _mailSettings.SenderFullName);
            mailMessage.To.Add(mail.ToEmail);
            mailMessage.Subject = mail.Subject;
            mailMessage.Body = mail.TextBody;
            smtpClient.Send(mailMessage);
        }
    }
}