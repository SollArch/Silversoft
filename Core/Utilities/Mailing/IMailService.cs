using Core.Mailing;

namespace Core.Utilities.Mailing
{
    public interface IMailService
    {
        void Send(Mail mail,string password);
    }
}