namespace Core.Mailing
{
    public class Mail
    {
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public string ToFullName { get; set; }
        public string ToEmail { get; set; }

        public Mail()
        {
        }

        public Mail(string subject, string textBody, string htmlBody, string toFullName,
            string toEmail)
        {
            Subject = subject;
            TextBody = textBody;
            ToFullName = toFullName;
            ToEmail = toEmail;
        }
    }
}