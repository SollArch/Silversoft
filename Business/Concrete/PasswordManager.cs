using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class PasswordManager : IPasswordService
{
    private readonly IPasswordDal _passwordDal;

    public PasswordManager(IPasswordDal passwordDal)
    {
        _passwordDal = passwordDal;
    }
    
    [SecuredOperation("admin")]
    public IResult AddPassword(string password)
    {
        var passwordToAdd = new AdminPassword
        {
            Password = password
        };
        _passwordDal.Add(passwordToAdd);
        return new SuccessResult();
    } 
    
    public string GetPassword()
    {
        var adminPassword = _passwordDal.GetAll()[0];
        return adminPassword.Password;
    }
}