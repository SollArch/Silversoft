namespace Core.Utilities.Mailing
{
    public class MailSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderFullName { get; set; }
        public string SenderEmail { get; set; }
        public string UserName { get; set; }

        public MailSettings()
        {
        }

        public MailSettings(string server, int port, string senderFullName, string senderEmail, string userName)
        {
            Server = server;
            Port = port;
            SenderFullName = senderFullName;
            SenderEmail = senderEmail;
            UserName = userName;
        }
    }
}