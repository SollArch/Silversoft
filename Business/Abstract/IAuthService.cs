namespace Business.Abstract
{
    public interface IAuthService
    {
        void SendNewPassword(string email,string name);
    }
}